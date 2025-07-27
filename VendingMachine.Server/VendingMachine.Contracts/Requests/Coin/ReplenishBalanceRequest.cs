using VendingMachine.Contracts.Dtos;

namespace VendingMachine.Contracts.Requests.Coin;

public record ReplenishBalanceRequest(DepositCoin[] Coins);