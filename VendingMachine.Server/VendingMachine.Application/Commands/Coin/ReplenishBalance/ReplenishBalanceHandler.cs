using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Repositories;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Application.Commands.Order.AddOrderItem;
using VendingMachine.Application.Extensions;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Coin.ReplenishBalance;

public class ReplenishBalanceHandler : ICommandHandler<ReplenishBalanceCommand>
{
    private readonly IValidator<ReplenishBalanceCommand> _validator; 
    
    private readonly ICoinRepository _coinRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<AddOrderItemCommand> _logger;

    public ReplenishBalanceHandler(IValidator<ReplenishBalanceCommand> validator, ICoinRepository coinRepository, IUnitOfWork unitOfWork, ILogger<AddOrderItemCommand> logger)
    {
        _validator = validator;
        _coinRepository = coinRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(ReplenishBalanceCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false) 
            return validationResult.ToErrorList();
        
        foreach (var depositCoin in command.Coins)
        {
            var coinResult = await _coinRepository.GetByDenominationAsync(Denomination.Of(depositCoin.Denomination).Value, cancellationToken);
            if (coinResult.IsFailure) 
                return coinResult.Error.ToErrorList();
            var coin = coinResult.Value;

            var addedResult = coin.AddStock(depositCoin.Quantity);
            if (addedResult.IsFailure) 
                return addedResult.Error.ToErrorList();
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("The balance has been replenished");

        return UnitResult.Success<ErrorList>();
    }
}