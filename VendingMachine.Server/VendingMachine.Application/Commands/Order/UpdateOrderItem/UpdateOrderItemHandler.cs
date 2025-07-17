using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Extensions;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Order.UpdateOrderItem;

public class UpdateOrderItemHandler : ICommandHandler<Guid, UpdateOrderItemCommand>
{
    private readonly IValidator<UpdateOrderItemCommand> _validator; 
    
    private readonly IRepository<Domain.Aggregates.Order,OrderId>  _orderRepository;
    
    private readonly IRepository<Domain.Entities.Product,ProductId>  _productRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<UpdateOrderItemHandler> _logger;

    public UpdateOrderItemHandler(IRepository<Domain.Aggregates.Order, OrderId> orderRepository, IRepository<Domain.Entities.Product, ProductId> productRepository, IUnitOfWork unitOfWork, ILogger<UpdateOrderItemHandler> logger, IValidator<UpdateOrderItemCommand> validator)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false) 
            return validationResult.ToErrorList();
        
        var quantityResult = Quantity.Of(command.Quantity);
        if (quantityResult.IsFailure) 
            return quantityResult.Error.ToErrorList();
        var quantity = quantityResult.Value;
        
        var orderId = OrderId.Of(command.OrderId);
        var orderResult = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        if (orderResult.IsFailure) 
            return orderResult.Error.ToErrorList();
        var order = orderResult.Value;
        
        var orderItemId = OrderItemId.Of(command.OrderItemId);
        var orderItemResult = order.GetOrderItemById(orderItemId);
        if (orderItemResult.IsFailure) 
            return orderItemResult.Error.ToErrorList();
        var orderItem = orderItemResult.Value;
        
        var productResult = await _productRepository.GetByIdAsync(orderItem.ProductId, cancellationToken);
        if (productResult.IsFailure)
            return productResult.Error.ToErrorList();
        var product = productResult.Value;
        
        product.AddStock(orderItem.Quantity.Value);
        product.SubstractStock(quantity.Value);
        
        var updateQuantityOrderItemResult = order.UpdateQuantityByOrderItemId(orderItemId, quantity);
        if (updateQuantityOrderItemResult.IsFailure)
            return updateQuantityOrderItemResult.Error.ToErrorList();
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            "Quantity product {productId} was updated in orderItem {orderItemId}",
            product.Id.Value,
            command.OrderItemId);

        return command.OrderItemId;
    }
}