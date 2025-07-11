using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Infrastructure.Configurations.Write;

public class CoinConfiguration : IEntityTypeConfiguration<Coin>
{
    public void Configure(EntityTypeBuilder<Coin> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, idGuid => CoinId.Of(idGuid));

        builder.Property(x => x.Denomination).IsRequired();
        
        builder.ComplexProperty(x => x.Stock, 
            p =>
            {
                p.Property(x => x.Value)
                    .HasColumnName(nameof(Coin.Stock))
                    .IsRequired();
            });
    }
}