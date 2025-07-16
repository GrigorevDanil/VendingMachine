using Microsoft.AspNetCore.Mvc;
using VendingMachine.API.Contracts.Session;
using VendingMachine.API.Controllers.Base;
using VendingMachine.API.Extensions;
using VendingMachine.Application.Commands.SessionCommands.OccupySession;
using VendingMachine.Application.Commands.SessionCommands.ReleaseSession;
using VendingMachine.Application.Queries.Session.GetBusy;

namespace VendingMachine.API.Controllers;

/// <summary>
/// Контроллер состояния занятости автомата
/// </summary>
public class SessionController:ApplicationController
{
    /// <summary>
    /// Получает занятость автомата
    /// </summary>
    /// <response code="200"></response>
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetBusyRequest request,
        [FromServices] GetBusyHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        
        var response = await handler.Handle(query, cancellationToken);
     
        return Ok(response);
    }
    
    /// <summary>
    /// Занять автомат
    /// </summary>
    /// <response code="200"></response>
    [HttpPost("occupy")]
    public async Task<ActionResult> Post(
        [FromForm] OccupySessionRequest request,
        [FromServices] OccupySessionHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        
        await handler.Handle(command, cancellationToken);

        return Ok();
    }
    
    /// <summary>
    /// Освободить автомат
    /// </summary>
    /// <response code="200"></response>
    [HttpPost("release")]
    public async Task<ActionResult> Post(
        [FromServices] ReleaseSessionHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new ReleaseSessionCommand();
        
        await handler.Handle(command, cancellationToken);

        return Ok();
    }
}