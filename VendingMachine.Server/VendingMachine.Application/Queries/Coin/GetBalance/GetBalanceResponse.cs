using VendingMachine.Contracts.Dtos.Database;

namespace VendingMachine.Application.Queries.Coin.GetBalance;

public record GetBalanceResponse(decimal Balance, CoinDto[] Coins);