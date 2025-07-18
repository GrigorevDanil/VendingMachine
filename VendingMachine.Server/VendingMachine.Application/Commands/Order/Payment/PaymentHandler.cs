using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Repositories;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Application.Extensions;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Order.Payment;

public class PaymentHandler : ICommandHandler<PaymentCoin[],PaymentCommand>
{
    private readonly IValidator<PaymentCommand> _validator; 
    
    private readonly IRepository<Domain.Aggregates.Order,OrderId> _orderRepository;
    
    private readonly ICoinRepository _coinRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<PaymentHandler> _logger;

    public PaymentHandler(IValidator<PaymentCommand> validator, IRepository<Domain.Aggregates.Order, OrderId> orderRepository, ICoinRepository coinRepository, IUnitOfWork unitOfWork, ILogger<PaymentHandler> logger)
    {
        _validator = validator;
        _orderRepository = orderRepository;
        _coinRepository = coinRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<PaymentCoin[], ErrorList>> Handle(PaymentCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false) 
            return validationResult.ToErrorList();
        
        var orderId = OrderId.Of(command.OrderId);
        var orderResult = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        if (orderResult.IsFailure) 
            return orderResult.Error.ToErrorList();
        var order =  orderResult.Value;

        if (order.Status == OrderStatus.Complete) 
            return Errors.Order.OrderAlreadyPayment().ToErrorList();
        
        var sumAcceptCoins = command.Coins.Sum(coin => coin.Denomination * coin.Quantity);

        if (order.TotalAmount.Value > sumAcceptCoins) 
            return Errors.Order.CoinsNotEnoughForPayment().ToErrorList();

        foreach (var paymentCoin in command.Coins)
        {
            var coinResult = await _coinRepository.GetByDenominationAsync(Denomination.Of(paymentCoin.Denomination).Value, cancellationToken);
            if (coinResult.IsFailure) 
                return coinResult.Error.ToErrorList();
            var coin = coinResult.Value;

            var addedResult = coin.AddStock(paymentCoin.Quantity);
            if (addedResult.IsFailure) 
                return addedResult.Error.ToErrorList();
            
        }
        
        var remains = sumAcceptCoins - order.TotalAmount.Value;
        var remainsCoins = new List<PaymentCoin>();

        if (remains > 0)
        {
            var availableCoins = await _coinRepository.GetAllByDescAsync(cancellationToken);
            var sumAvailableCoins = availableCoins.Sum(x => x.Denomination.Value * x.Stock.Value);
            
            if (sumAvailableCoins < remains) 
                return Errors.Order.NotEnoughAvailableCoins().ToErrorList();
            
            foreach (var coin in availableCoins)
            {
                if (remains <= 0) break;

                var denomination = coin.Denomination.Value;
                var maxPossible = (int)(remains / denomination);
            
                if (maxPossible > 0 && coin.Stock.Value > 0)
                {
                    var toReturn = Math.Min(maxPossible, coin.Stock.Value);
                    remains -= toReturn * denomination;
                    
                    var subtractResult = coin.SubtrackStock(toReturn);
                    if (subtractResult.IsFailure)
                        return subtractResult.Error.ToErrorList();
                
                    remainsCoins.Add(new PaymentCoin(denomination, toReturn));
                }
            }
        }
        
        order.Complete();
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Order '{orderId}' has been completed", order.Id.Value);

        return remainsCoins.ToArray();
    }
}