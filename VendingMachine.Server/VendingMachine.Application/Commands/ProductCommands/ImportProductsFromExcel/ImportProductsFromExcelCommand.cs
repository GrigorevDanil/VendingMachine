using Microsoft.AspNetCore.Http;
using VendingMachine.Application.Abstractions;

namespace VendingMachine.Application.Commands.ProductCommands.ImportProductsFromExcel;

public record ImportProductsFromExcelCommand(IFormFile File): ICommand;