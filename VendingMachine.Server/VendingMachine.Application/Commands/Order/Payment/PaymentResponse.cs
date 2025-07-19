namespace VendingMachine.Application.Commands.Order.Payment;

public record PaymentResponse(decimal Remains, PaymentCoin[]  Coins);