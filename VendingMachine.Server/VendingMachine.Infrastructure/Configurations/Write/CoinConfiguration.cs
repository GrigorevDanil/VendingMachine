using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Infrastructure.Configurations.Write;

public class CoinConfiguration : IEntityTypeConfiguration<Coin>
{
    public void Configure(EntityTypeBuilder<Coin> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, idGuid => CoinId.Of(idGuid))
            .HasDefaultValueSql("gen_random_uuid()");

        builder.ComplexProperty(x => x.Denomination, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(Coin.Denomination))
                    .HasMaxLength(Denomination.MAX_LENGTH)
                    .IsRequired();
            });
        
        builder.ComplexProperty(x => x.Stock, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(Coin.Stock))
                    .IsRequired();
            });
    }
}