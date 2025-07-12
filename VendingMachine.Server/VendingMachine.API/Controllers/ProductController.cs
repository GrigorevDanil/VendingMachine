using Microsoft.AspNetCore.Mvc;
using VendingMachine.API.Contracts.Product.Requests;
using VendingMachine.API.Controllers.Base;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.Models;
using VendingMachine.Application.Queries.GetProductsWithPagination;

namespace VendingMachine.API.Controllers;

/// <summary>
/// Контроллер для работы с товарами (напитками)
/// </summary>
[ApiController]
[Route("api/products")]
public class ProductController : ApplicationController
{
    /// <summary>
    /// Получает список товаров с пагинацией и фильтрами
    /// </summary>
    /// <response code="200"></response>
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetProductsWithPaginationRequest request,
        [FromServices] GetProductsWithPaginationHandler withPaginationHandler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        
        var response = await withPaginationHandler.Handle(query, cancellationToken);
     
        return Ok(response);
    }
}