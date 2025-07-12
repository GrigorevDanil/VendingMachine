using Microsoft.AspNetCore.Mvc;
using VendingMachine.API.Contracts.Product;
using VendingMachine.API.Controllers.Base;
using VendingMachine.Application.Queries.Product.GetProductsWithPagination;

namespace VendingMachine.API.Controllers;

/// <summary>
/// Контроллер для работы с товарами (напитками)
/// </summary>
public class ProductController : ApplicationController
{
    /// <summary>
    /// Получает список товаров с пагинацией и фильтрами
    /// </summary>
    /// <response code="200"></response>
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetProductsWithPaginationRequest request,
        [FromServices] GetProductsWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        
        var response = await handler.Handle(query, cancellationToken);
     
        return Ok(response);
    }
}