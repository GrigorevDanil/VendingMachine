
using CSharpFunctionalExtensions;

namespace VendingMachine.Application.Dtos;

public class BrandDto : Entity<Guid>
{
    public string Title { get; init; } = string.Empty;
    public ProductDto[] Products { get; init; } = [];
}