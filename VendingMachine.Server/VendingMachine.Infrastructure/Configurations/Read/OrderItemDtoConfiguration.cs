using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Application.Dtos;

namespace VendingMachine.Infrastructure.Configurations.Read;

public class OrderItemDtoConfiguration : IEntityTypeConfiguration<OrderItemDto>
{
    public void Configure(EntityTypeBuilder<OrderItemDto> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne<ProductDto>().WithMany().HasForeignKey(x => x.ProductId);
        
    }
}