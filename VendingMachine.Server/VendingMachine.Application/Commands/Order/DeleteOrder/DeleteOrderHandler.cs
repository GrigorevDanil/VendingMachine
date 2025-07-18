using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Application.Extensions;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Order.DeleteOrder;

public class DeleteOrderHandler : ICommandHandler<Guid,DeleteOrderCommand>
{
    private readonly IValidator<DeleteOrderCommand> _validator; 
    
    private readonly IRepository<Domain.Aggregates.Order,OrderId>  _orderRepository;
    
    private readonly IRepository<Domain.Entities.Product, ProductId>  _productRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<DeleteOrderHandler> _logger;

    public DeleteOrderHandler(IRepository<Domain.Aggregates.Order, OrderId> orderRepository, IUnitOfWork unitOfWork, ILogger<DeleteOrderHandler> logger, IRepository<Domain.Entities.Product, ProductId> productRepository, IValidator<DeleteOrderCommand> validator)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _productRepository = productRepository;
        _validator = validator;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(DeleteOrderCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false) 
            return validationResult.ToErrorList();
        
        var orderId = OrderId.Of(command.OrderId);
        var orderResult = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        if (orderResult.IsFailure) 
            return orderResult.Error.ToErrorList();
        var order = orderResult.Value;

        foreach (var item in order.Items)
        {
            var productResult = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
            if (productResult.IsFailure) 
                return productResult.Error.ToErrorList();
            var product =  productResult.Value;
            
            product.AddStock(item.Quantity.Value);
        }
        
        _orderRepository.Delete(order);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Order with id {moduleId} deleted", command.OrderId);

        return command.OrderId;
    }
}