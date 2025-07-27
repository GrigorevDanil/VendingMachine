namespace VendingMachine.Contracts.Responses;

public record OrderWithItemsResponse(Guid OrderId, Guid[] OrderItemIds);