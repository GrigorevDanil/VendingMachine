using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Commands.Order.Payment;
using VendingMachine.Application.Dtos;

namespace VendingMachine.Application.Commands.Coin.ReplenishBalance;

public record ReplenishBalanceCommand(DepositCoin[] Coins) : ICommand;