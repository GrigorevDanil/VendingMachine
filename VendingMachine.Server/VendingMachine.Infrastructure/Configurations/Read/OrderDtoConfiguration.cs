using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Application.Dtos;

namespace VendingMachine.Infrastructure.Configurations.Read;

public class OrderDtoConfiguration: IEntityTypeConfiguration<OrderDto>
{
    public void Configure(EntityTypeBuilder<OrderDto> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.OrderId);
    }
}