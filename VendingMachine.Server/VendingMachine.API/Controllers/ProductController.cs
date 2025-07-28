using Microsoft.AspNetCore.Mvc;
using VendingMachine.API.Controllers.Base;
using VendingMachine.API.Extensions;
using VendingMachine.Application.Commands.Product.ImportProductsFromExcel;
using VendingMachine.Application.Commands.Product.UpdateProductStock;
using VendingMachine.Application.Queries.Product.GetProductsWithPagination;
using VendingMachine.Contracts.Requests.Product;

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
        var query = new GetProductWithPaginationQuery(
            request.BrandId,
            request.Title,
            request.MinPrice,
            request.SortBy,
            request.SortDirection,
            request.Page,
            request.PageSize
            );
        
        var response = await handler.Handle(query, cancellationToken);
     
        return Ok(response);
    }
    
    /// <summary>
    /// Импорт товаров из Excel файла (BrandId, Title, Price, Stock, ImageUrl - необходимые столбцы)
    /// </summary>
    /// <response code="200"></response>
    [HttpPost("import")]
    public async Task<ActionResult> Post( [FromForm] ImportProductsFromExcelRequest request,
        [FromServices] ImportProductsFromExcelHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new ImportProductsFromExcelCommand(
            request.File);
        
        var result = await handler.Handle(command,cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    /// <summary>
    /// Обновить количество товара
    /// </summary>
    /// <response code="200"></response>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateStockProduct(
        [FromRoute] Guid id,
        [FromForm] UpdateProductStockRequest request,
        [FromServices] UpdateProductStockHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProductStockCommand(
            id, 
            request.Stock
        );
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}   