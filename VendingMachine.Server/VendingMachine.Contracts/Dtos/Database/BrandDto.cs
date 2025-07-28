
namespace VendingMachine.Contracts.Dtos.Database;

public class BrandDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public ProductDto[] Products { get; init; } = [];
}