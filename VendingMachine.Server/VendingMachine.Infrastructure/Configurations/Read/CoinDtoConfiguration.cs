using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Contracts.Dtos.Database;

namespace VendingMachine.Infrastructure.Configurations.Read;

public class CoinDtoConfiguration : IEntityTypeConfiguration<CoinDto>
{
    public void Configure(EntityTypeBuilder<CoinDto> builder)
    {
        builder.ToTable("Coins");
        
        builder.HasKey(x => x.Id);
    }
}