using Microsoft.AspNetCore.Http;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Product.ImportProductsFromExcel;

public record ImportProductsFromExcelCommand(IFormFile File): ICommand;