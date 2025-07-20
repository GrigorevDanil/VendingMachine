using ICommand = VendingMachine.Application.Abstractions.Messages.ICommand;

namespace VendingMachine.Application.Commands.Product.UpdateProductStock;

public record UpdateProductStockCommand(Guid ProductId, int Stock) : ICommand;