using CSharpFunctionalExtensions;

namespace VendingMachine.Application.Dtos;

public class CoinDto: Entity<Guid>
{
    public int Denomination { get; init; }
    
    public int Stock { get; init; }
}