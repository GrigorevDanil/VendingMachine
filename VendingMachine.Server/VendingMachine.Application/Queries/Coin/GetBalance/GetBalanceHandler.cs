using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Queries.Coin.GetBalance;

public class GetBalanceHandler : IQueryHandler<GetBalanceResponse>
{
    private readonly IReadDbContext _dbContext;

    public GetBalanceHandler(IReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetBalanceResponse> Handle(CancellationToken cancellationToken = default)
    {
        var coinQuery = _dbContext.Coins;

        var balance = await coinQuery.Select(p => p.Denomination * p.Stock).SumAsync(cancellationToken);

        var coins = await coinQuery.ToArrayAsync(cancellationToken);
        
        return new GetBalanceResponse(balance, coins );
    }
}