using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Order.AddOrderItem;

public record AddOrderItemCommand(Guid OrderId, Guid ProductId, int Quantity) : ICommand;