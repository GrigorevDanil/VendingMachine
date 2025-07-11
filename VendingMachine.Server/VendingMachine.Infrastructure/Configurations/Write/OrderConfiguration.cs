using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Domain.Aggregates;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Infrastructure.Configurations.Write;

public class OrderConfiguration: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, idGuid => OrderId.Of(idGuid));
        
        builder.ComplexProperty(x => x.CreatedAt, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(Order.CreatedAt))
                    .IsRequired();
            });
        
        builder.ComplexProperty(x => x.TotalAmount, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(Order.TotalAmount))
                    .IsRequired();
            });
        
        builder.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.OrderId);
    }
}