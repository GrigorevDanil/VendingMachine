using Microsoft.AspNetCore.Mvc;
using SachkovTech.Framework;
using VendingMachine.API.Contracts.Image;
using VendingMachine.API.Controllers.Base;
using VendingMachine.Application.Commands.Image;

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
    public async Task<ActionResult> Post( [FromForm] DownloadImageRequest request,
        [FromServices] DownloadImageHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        
        var result = await handler.Handle(command,cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}