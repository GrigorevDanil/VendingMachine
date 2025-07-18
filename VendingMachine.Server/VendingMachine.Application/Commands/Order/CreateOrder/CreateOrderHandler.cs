using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Order.CreateOrder;

public class CreateOrderHandler : ICommandHandler<Guid, CreateOrderCommand>
{
    private readonly IRepository<Domain.Aggregates.Order,OrderId>  _orderRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandler(IRepository<Domain.Aggregates.Order, OrderId> orderRepository, IUnitOfWork unitOfWork, ILogger<CreateOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(CreateOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var order = new Domain.Aggregates.Order(
            OrderId.Create(),
            OrderStatus.AwaitPayment, 
            OrderDate.Create().Value,
            TotalAmount.Of(0).Value,
            []);
        
        await _orderRepository.AddAsync(order, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Created order with id {order.Id.Value}", order.Id.Value);
        
        return order.Id.Value;
    }
}