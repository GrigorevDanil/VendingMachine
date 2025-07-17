using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Order.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : ICommand;