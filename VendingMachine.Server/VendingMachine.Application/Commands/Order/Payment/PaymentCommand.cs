using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Dtos;

namespace VendingMachine.Application.Commands.Order.Payment;

public record PaymentCommand(Guid OrderId, DepositCoin[] Coins) : ICommand;

