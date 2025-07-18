using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Order.Payment;

public record PaymentCommand(Guid OrderId, PaymentCoin[] Coins) : ICommand;

