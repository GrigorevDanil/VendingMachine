
namespace VendingMachine.Application.Commands.Order.CreateOrderWithItems;

public record CreateOrderWithItemsResponse(Guid OrderId, Guid[] OrderItemIds);