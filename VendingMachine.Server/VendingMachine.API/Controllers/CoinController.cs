using Microsoft.AspNetCore.Mvc;
using VendingMachine.API.Controllers.Base;
using VendingMachine.API.Extensions;
using VendingMachine.Application.Commands.Coin.ReplenishBalance;
using VendingMachine.Application.Queries.Coin.GetBalance;
using VendingMachine.Contracts.Requests.Coin;

namespace VendingMachine.API.Controllers;

/// <summary>
/// Контроллер для работы с монетами
/// </summary>
public class CoinController : ApplicationController
{
    /// <summary>
    /// Получает баланс и монеты
    /// </summary>
    /// <response code="200"></response>
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromServices] GetBalanceHandler handler,
        CancellationToken cancellationToken)
    {
        var response = await handler.Handle(cancellationToken);
     
        return Ok(response);
    }
    
    /// <summary>
    /// Пополнить баланс
    /// </summary>
    /// <response code="200"></response>
    [HttpPost]
    public async Task<ActionResult> Post(
        [FromBody] ReplenishBalanceRequest request,
        [FromServices] ReplenishBalanceHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new ReplenishBalanceCommand(
            request.Coins
            );
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }
}