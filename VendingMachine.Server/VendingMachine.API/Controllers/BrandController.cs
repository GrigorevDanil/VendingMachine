using Microsoft.AspNetCore.Mvc;
using VendingMachine.API.Controllers.Base;
using VendingMachine.Application.Queries.Brand.GetBrands;

namespace VendingMachine.API.Controllers;

/// <summary>
/// Контроллер для работы с брендами
/// </summary>
public class BrandController :ApplicationController
{
    /// <summary>
    /// Получает список брендов
    /// </summary>
    /// <response code="200"></response>
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromServices] GetBrandsHandler handler,
        CancellationToken cancellationToken)
    {
        var response = await handler.Handle(cancellationToken);
     
        return Ok(response);
    }
}