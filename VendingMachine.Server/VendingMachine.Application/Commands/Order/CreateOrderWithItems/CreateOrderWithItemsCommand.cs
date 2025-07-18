using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Order.CreateOrderWithItems;

public record CreateOrderWithItemsCommand(CreateOrderWithItemsOrderItem[] OrderItems): ICommand;

public record CreateOrderWithItemsOrderItem(Guid ProductId, int Quantity);