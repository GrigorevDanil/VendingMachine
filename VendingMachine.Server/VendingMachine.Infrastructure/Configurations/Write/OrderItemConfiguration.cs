using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Infrastructure.Configurations.Write;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, idGuid => OrderItemId.Of(idGuid))
            .HasDefaultValueSql("gen_random_uuid()");
        
        builder.ComplexProperty(x => x.BrandTitle, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(OrderItem.BrandTitle))
                    .HasMaxLength(Title.MAX_LENGTH)
                    .IsRequired();
            });
        
        builder.ComplexProperty(x => x.ProductTitle, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(OrderItem.ProductTitle))
                    .HasMaxLength(Title.MAX_LENGTH)
                    .IsRequired();
            });
        
        builder.ComplexProperty(x => x.ProductPrice, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(OrderItem.ProductPrice))
                    .IsRequired();
            });
        
        builder.ComplexProperty(x => x.Quantity, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(OrderItem.Quantity))
                    .IsRequired();
            });
        
        builder.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId);
        
    }
}