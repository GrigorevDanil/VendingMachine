using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Application.Dtos;

namespace VendingMachine.Infrastructure.Configurations.Read;

public class BrandDtoConfiguration : IEntityTypeConfiguration<BrandDto>
{
    public void Configure(EntityTypeBuilder<BrandDto> builder)
    {
        builder.ToTable("Brands");
        
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Products).WithOne().HasForeignKey(x => x.BrandId);
    }
}