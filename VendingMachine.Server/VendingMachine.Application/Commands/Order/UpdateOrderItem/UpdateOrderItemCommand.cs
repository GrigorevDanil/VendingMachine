using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Order.UpdateOrderItem;

public record UpdateOrderItemCommand(Guid OrderId, Guid OrderItemId, int Quantity) : ICommand;