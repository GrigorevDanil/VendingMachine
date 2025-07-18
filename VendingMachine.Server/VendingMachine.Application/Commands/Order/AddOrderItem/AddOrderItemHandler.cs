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

namespace VendingMachine.Application.Commands.Order.AddOrderItem;

public class AddOrderItemHandler : ICommandHandler<Guid, AddOrderItemCommand>
{
    private readonly IValidator<AddOrderItemCommand> _validator; 
    
    private readonly IRepository<Domain.Aggregates.Order,OrderId>  _orderRepository;
    
    private readonly IRepository<Domain.Entities.Product,ProductId>  _productRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<AddOrderItemCommand> _logger;

    public AddOrderItemHandler(IValidator<AddOrderItemCommand> validator, IRepository<Domain.Aggregates.Order, OrderId> orderRepository, IRepository<Domain.Entities.Product, ProductId> productRepository, IUnitOfWork unitOfWork, ILogger<AddOrderItemCommand> logger)
    {
        _validator = validator;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(AddOrderItemCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false) 
            return validationResult.ToErrorList();
        
        var productId = ProductId.Of(command.ProductId);
        var productResult = await _productRepository.GetByIdAsync(productId, cancellationToken);
        if (productResult.IsFailure) 
            return productResult.Error.ToErrorList();
        var product = productResult.Value;

        if (product.Stock.Value < command.Quantity) 
            return Errors.Order.ProductNotEnough(command.ProductId).ToErrorList();
        
        var orderId = OrderId.Of(command.OrderId);
        var orderResult = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        if (orderResult.IsFailure) 
            return orderResult.Error.ToErrorList();
        var order =  orderResult.Value;

        var orderItem = new OrderItem(
            OrderItemId.Create(),
            product.Brand.Title,
            product.Title,
            product.Price,
            Quantity.Of(command.Quantity).Value,
            orderId,
            productId
            );
        
        order.AddItem(orderItem);
        
        product.SubstractStock(command.Quantity);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            "OrderItem {orderItemId} was created in order {orderId}",
            orderItem.Id.Value,
            command.OrderId);

        return orderItem.Id.Value;
    }
}