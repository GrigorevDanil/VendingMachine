namespace VendingMachine.Application.Responses;

public record OrderWithItemsResponse(Guid OrderId, Guid[] OrderItemIds);