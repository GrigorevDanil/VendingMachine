using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Order.DeleteOrderItem;

public record DeleteOrderItemCommand(Guid OrderId, Guid OrderItemId) : ICommand;