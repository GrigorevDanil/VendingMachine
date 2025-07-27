
using CSharpFunctionalExtensions;

namespace VendingMachine.Contracts.Dtos.Database;

public class BrandDto : Entity<Guid>
{
    public string Title { get; init; } = string.Empty;
    public ProductDto[] Products { get; init; } = [];
}