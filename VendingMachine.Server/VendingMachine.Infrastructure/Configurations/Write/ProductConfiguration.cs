﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Infrastructure.Configurations.Write;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, idGuid => ProductId.Of(idGuid))
            .HasDefaultValueSql("gen_random_uuid()");
        
        builder.ComplexProperty(x => x.ImageUrl, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(Product.ImageUrl))
                    .HasMaxLength(ImageUrl.MAX_LENGTH)
                    .IsRequired();
            });
        
        builder.ComplexProperty(x => x.Title, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(Product.Title))
                    .HasMaxLength(Title.MAX_LENGTH)
                    .IsRequired();
            });
        
        builder.ComplexProperty(x => x.Price, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(Product.Price))
                    .IsRequired();
            });
        
        builder.ComplexProperty(x => x.Stock, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(Product.Stock))
                    .IsRequired();
            });
    }
}