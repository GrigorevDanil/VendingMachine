using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Contracts.Dtos;

namespace VendingMachine.Application.Commands.Coin.ReplenishBalance;

public record ReplenishBalanceCommand(DepositCoin[] Coins) : ICommand;