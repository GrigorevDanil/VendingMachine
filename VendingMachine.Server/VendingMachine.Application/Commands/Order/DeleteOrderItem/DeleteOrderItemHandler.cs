using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Extensions;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Order.DeleteOrderItem;

public class DeleteOrderItemHandler : ICommandHandler<Guid, DeleteOrderItemCommand>
{
    private readonly IValidator<DeleteOrderItemCommand> _validator; 
    
    private readonly IRepository<Domain.Aggregates.Order,OrderId>  _orderRepository;
    
    private readonly IRepository<Domain.Entities.Product, ProductId>  _productRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<DeleteOrderItemHandler> _logger;

    public DeleteOrderItemHandler(IRepository<Domain.Aggregates.Order, OrderId> orderRepository, IRepository<Domain.Entities.Product, ProductId> productRepository, IUnitOfWork unitOfWork, ILogger<DeleteOrderItemHandler> logger, IValidator<DeleteOrderItemCommand> validator)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(DeleteOrderItemCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false) 
            return validationResult.ToErrorList();
        
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
        
        var deleteOrderItemResult = order.DeleteOrderItemById(orderItemId);
        if (deleteOrderItemResult.IsFailure)
            return deleteOrderItemResult.Error.ToErrorList();
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            "OrderItem {orderItemId} was deleted from order {orderId}",
            command.OrderItemId,
            command.OrderId);
        
        return command.OrderItemId;
    }
}