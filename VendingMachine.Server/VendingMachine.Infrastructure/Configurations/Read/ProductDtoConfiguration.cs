using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Contracts.Dtos.Database;

namespace VendingMachine.Infrastructure.Configurations.Read;

public class ProductDtoConfiguration : IEntityTypeConfiguration<ProductDto>
{
    public void Configure(EntityTypeBuilder<ProductDto> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(x => x.Id);
        
    }
}