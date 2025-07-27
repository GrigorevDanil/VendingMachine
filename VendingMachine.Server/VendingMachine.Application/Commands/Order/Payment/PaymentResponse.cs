
using VendingMachine.Contracts.Dtos;

namespace VendingMachine.Application.Commands.Order.Payment;

public record PaymentResponse(decimal Remains, DepositCoin[]  Coins);