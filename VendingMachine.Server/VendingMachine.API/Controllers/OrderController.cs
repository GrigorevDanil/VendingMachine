using Microsoft.AspNetCore.Mvc;
using VendingMachine.API.Contracts.Order;
using VendingMachine.API.Controllers.Base;
using VendingMachine.API.Extensions;
using VendingMachine.Application.Commands.Order.AddOrderItem;
using VendingMachine.Application.Commands.Order.CreateOrder;
using VendingMachine.Application.Commands.Order.CreateOrderWithItems;
using VendingMachine.Application.Commands.Order.DeleteOrder;
using VendingMachine.Application.Commands.Order.DeleteOrderItem;
using VendingMachine.Application.Commands.Order.Payment;
using VendingMachine.Application.Commands.Order.UpdateOrderItem;

namespace VendingMachine.API.Controllers;

/// <summary>
/// Контроллер для работы с заказами
/// </summary>
public class OrderController : ApplicationController
{
    /// <summary>
    /// Создать заказ
    /// </summary>
    /// <response code="200"></response>
    [HttpPost]
    public async Task<ActionResult> Post(
        [FromServices] CreateOrderHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand();
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    /// <summary>
    /// Создать заказ с товарами
    /// </summary>
    /// <response code="200"></response>
    [HttpPost("multiple")]
    public async Task<ActionResult> AddOrderWithItems(
        [FromBody] CreateOrderWithItemsRequest request,
        [FromServices] CreateOrderWithItemsHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    /// <summary>
    /// Оплатить заказ
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <response code="200"></response>
    [HttpPost("payment/{id:guid}")]
    public async Task<ActionResult> PaymentOrder(
        [FromRoute] Guid id,
        [FromBody] PaymentRequest request,
        [FromServices] PaymentHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    /// <summary>
    /// Удалить заказ (После удаления количество товара возвращается)
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <response code="200"></response>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteOrderHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new  DeleteOrderCommand(id);
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    /// <summary>
    /// Добавить товар в заказ
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <response code="200"></response>
    [HttpPost("{id:guid}/orderItem")]
    public async Task<ActionResult> AddOrderItem(
        [FromRoute] Guid id,
        [FromForm] AddOrderItemRequest request,
        [FromServices] AddOrderItemHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    /// <summary>
    /// Удалить товар из заказа (После удаления количество товара возвращается)
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <param name="orderItemId">Идентификатор товара в заказе</param>
    /// <response code="200"></response>
    [HttpDelete("{id:guid}/orderItem/{orderItemId:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromRoute] Guid orderItemId,
        [FromServices] DeleteOrderItemHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new  DeleteOrderItemCommand(id, orderItemId);
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    /// <summary>
    /// Обновить количество товара в заказе
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <param name="orderItemId">Идентификатор товара в заказе</param>
    /// <response code="200"></response>
    [HttpPut("{id:guid}/orderItem/{orderItemId:guid}")]
    public async Task<ActionResult> UpdateQuantity(
        [FromRoute] Guid id,
        [FromRoute] Guid orderItemId,
        [FromForm] UpdateOrderItemRequest request,
        [FromServices] UpdateOrderItemHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id,orderItemId);
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}