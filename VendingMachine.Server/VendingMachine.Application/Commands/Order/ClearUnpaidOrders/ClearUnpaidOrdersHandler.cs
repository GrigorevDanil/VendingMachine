using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Repositories;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Order.ClearUnpaidOrders;

public class ClearUnpaidOrdersHandler : ICommandHandler<ClearUnpaidOrdersCommand>
{
    private readonly IOrderRepository  _orderRepository;
    
    private readonly IRepository<Domain.Entities.Product,ProductId>  _productRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<ClearUnpaidOrdersCommand> _logger;

    public ClearUnpaidOrdersHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, ILogger<ClearUnpaidOrdersCommand> logger, IRepository<Domain.Entities.Product, ProductId> productRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<UnitResult<ErrorList>> Handle(ClearUnpaidOrdersCommand command, CancellationToken cancellationToken = default)
    {
        var unpaidOrders = await _orderRepository.GetAllUnpaidAsync(cancellationToken);

        foreach (var order in unpaidOrders)
        {
            foreach (var orderItem in order.Items)
            {
                var productResult = await _productRepository.GetByIdAsync(orderItem.ProductId, cancellationToken);
                if (productResult.IsFailure) 
                    return productResult.Error.ToErrorList();
                var product = productResult.Value;
                
                var addedStockResult = product.AddStock(orderItem.Quantity.Value);
                if (addedStockResult.IsFailure)
                    return addedStockResult.Error.ToErrorList();
            }
            
            _orderRepository.Delete(order);
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("All unpaid order was be deleted");

        return UnitResult.Success<ErrorList>();
    }
}