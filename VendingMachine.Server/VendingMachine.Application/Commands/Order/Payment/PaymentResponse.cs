using VendingMachine.Application.Dtos;

namespace VendingMachine.Application.Commands.Order.Payment;

public record PaymentResponse(decimal Remains, DepositCoin[]  Coins);