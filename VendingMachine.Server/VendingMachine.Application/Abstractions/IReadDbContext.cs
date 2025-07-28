
using VendingMachine.Contracts.Dtos.Database;

namespace VendingMachine.Application.Abstractions;

public interface IReadDbContext
{
    IQueryable<BrandDto> Brands { get; }
    
    IQueryable<CoinDto> Coins { get; }
    
    IQueryable<OrderDto> Orders { get; }
    
    IQueryable<OrderItemDto> OrderItems { get; }
    
    IQueryable<ProductDto> Products { get; }
}