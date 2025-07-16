using Microsoft.AspNetCore.Mvc;
using VendingMachine.API.Contracts.Image;
using VendingMachine.API.Controllers.Base;
using VendingMachine.API.Extensions;
using VendingMachine.Application.Commands.ImageCommands.AddImage;
using VendingMachine.Application.Commands.ImageCommands.RemoveImageByName;

namespace VendingMachine.API.Controllers;

/// <summary>
/// Контроллер для работы с изображениями
/// </summary>
public class ImageController : ApplicationController
{
    /// <summary>
    /// Сохранить изображение в сервер
    /// </summary>
    /// <response code="200"></response>
    [HttpPost]
    public async Task<ActionResult> Post( [FromForm] AddImageRequest request,
        [FromServices] AddImageHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        
        var result = await handler.Handle(command,cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    /// <summary>
    /// Удалить изображение из сервера
    /// </summary>
    /// <response code="200"></response>
    [HttpDelete]
    public async Task<ActionResult> Delete( [FromForm] RemoveImageByNameRequest request,
        [FromServices] RemoveImageByNameHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        
        var result = await handler.Handle(command,cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }
}