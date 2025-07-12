using CSharpFunctionalExtensions;

namespace VendingMachine.Application.Dtos;

public class OrderDto: Entity<Guid>
{
    public DateTime CreatedAt { get; init; }
    
    public decimal TotalAmount {get; init;}
    
    public OrderItemDto[] Items { get; init; } = [];
}