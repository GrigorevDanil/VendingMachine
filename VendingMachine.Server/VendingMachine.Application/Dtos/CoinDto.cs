using CSharpFunctionalExtensions;

namespace VendingMachine.Application.Dtos;

public class CoinDto: Entity<Guid>
{
    public string Denomination { get; init; } = string.Empty;
    
    public int Stock { get; init; }
}