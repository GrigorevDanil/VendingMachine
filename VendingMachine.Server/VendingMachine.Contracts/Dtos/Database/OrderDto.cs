namespace VendingMachine.Contracts.Dtos.Database;

public class OrderDto
{
    public Guid Id { get; init; }
    
    public string Status { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public int TotalAmount {get; init;}
    
    public OrderItemDto[] Items { get; init; } = [];
}