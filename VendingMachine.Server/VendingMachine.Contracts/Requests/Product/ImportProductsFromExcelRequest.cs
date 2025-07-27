using Microsoft.AspNetCore.Http;

namespace VendingMachine.Contracts.Requests.Product;

/// <summary>
/// Запрос файла для импорта данных из Excel
/// </summary>
/// <param name="File">Файл Excel</param>
public record ImportProductsFromExcelRequest(IFormFile File);