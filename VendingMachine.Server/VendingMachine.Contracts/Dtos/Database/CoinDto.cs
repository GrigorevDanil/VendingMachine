
namespace VendingMachine.Contracts.Dtos.Database;

public class CoinDto
{
    public Guid Id { get; init; }
    
    public int Denomination { get; init; }
    
    public int Stock { get; init; }
}