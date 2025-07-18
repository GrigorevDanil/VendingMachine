using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Application.Extensions;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Order.CreateOrderWithItems;

public class CreateOrderWithItemsHandler : ICommandHandler<CreateOrderWithItemsResponse, CreateOrderWithItemsCommand>
{
    private readonly IValidator<CreateOrderWithItemsCommand> _validator; 
    
    private readonly IRepository<Domain.Aggregates.Order,OrderId>  _orderRepository;
    
    private readonly IRepository<Domain.Entities.Product,ProductId>  _productRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<CreateOrderWithItemsCommand> _logger;

    public CreateOrderWithItemsHandler(IValidator<CreateOrderWithItemsCommand> validator, IRepository<Domain.Aggregates.Order, OrderId> orderRepository, IRepository<Domain.Entities.Product, ProductId> productRepository, IUnitOfWork unitOfWork, ILogger<CreateOrderWithItemsCommand> logger)
    {
        _validator = validator;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<CreateOrderWithItemsResponse, ErrorList>> Handle(CreateOrderWithItemsCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false) 
            return validationResult.ToErrorList();
        
        var order = new Domain.Aggregates.Order(
            OrderId.Create(),
            OrderStatus.AwaitPayment, 
            OrderDate.Create().Value,
            TotalAmount.Of(0).Value,
            []);

        foreach (var orderItemFromCommand in command.OrderItems)
        {
            var productId = ProductId.Of(orderItemFromCommand.ProductId);
            var productResult = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (productResult.IsFailure) 
                return productResult.Error.ToErrorList();
            var product = productResult.Value;

            if (product.Stock.Value < orderItemFromCommand.Quantity) 
                return Errors.Order.ProductNotEnough(orderItemFromCommand.ProductId).ToErrorList();
            
            var orderItem = new OrderItem(
                OrderItemId.Create(),
                product.Brand.Title, 
                product.Title,
                product.Price,
                Quantity.Of(orderItemFromCommand.Quantity).Value, 
                order.Id, productId);
            
            order.AddItem(orderItem);
            
            product.SubstractStock(orderItemFromCommand.Quantity);
        }
        
        await _orderRepository.AddAsync(order, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            "Order {OrderId} was created. Added items: {OrderItems}",
            order.Id.Value,
            string.Join(", ", order.Items.Select(x => x.Id.Value)));

        return new CreateOrderWithItemsResponse(
            order.Id.Value, 
            order.Items.Select(x => x.Id.Value).ToArray());
    }
}