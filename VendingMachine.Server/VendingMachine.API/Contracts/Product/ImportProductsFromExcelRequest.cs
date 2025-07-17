using VendingMachine.Application.Commands.Product.ImportProductsFromExcel;

namespace VendingMachine.API.Contracts.Product;

/// <summary>
/// Запрос файла для импорта данных из Excel
/// </summary>
/// <param name="File">Файл Excel</param>
public record ImportProductsFromExcelRequest(IFormFile File)
{
    public ImportProductsFromExcelCommand ToCommand() => 
        new(File);
}