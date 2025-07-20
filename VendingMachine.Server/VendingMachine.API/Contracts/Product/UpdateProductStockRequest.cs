using VendingMachine.Application.Commands.Product.UpdateProductStock;

namespace VendingMachine.API.Contracts.Product;

public record UpdateProductStockRequest(int Stock)
{
    public UpdateProductStockCommand ToCommand(Guid productId) => new(productId, Stock);
}