using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Contracts.Dtos;

namespace VendingMachine.Application.Commands.Order.Payment;

public record PaymentCommand(Guid OrderId, DepositCoin[] Coins) : ICommand;

