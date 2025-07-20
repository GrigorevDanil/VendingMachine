using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.Dtos.Database;

namespace VendingMachine.Infrastructure.Configurations.Read;

public class OrderItemDtoConfiguration : IEntityTypeConfiguration<OrderItemDto>
{
    public void Configure(EntityTypeBuilder<OrderItemDto> builder)
    {
        builder.ToTable("OrderItems");
        
        builder.HasKey(x => x.Id);
        
        builder.HasOne<ProductDto>().WithMany().HasForeignKey(x => x.ProductId);
        
    }
}