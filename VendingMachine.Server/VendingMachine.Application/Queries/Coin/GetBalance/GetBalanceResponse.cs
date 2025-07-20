using VendingMachine.Application.Dtos;
using VendingMachine.Application.Dtos.Database;

namespace VendingMachine.Application.Queries.Coin.GetBalance;

public record GetBalanceResponse(decimal Balance, CoinDto[] Coins);