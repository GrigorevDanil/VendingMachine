using CSharpFunctionalExtensions;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Product.ImportProductsFromExcel;

public class ImportProductsFromExcelHandler : ICommandHandler<ProductId[],ImportProductsFromExcelCommand>
{
    private readonly IExcelProductImportService _excelProductImportService;

    public ImportProductsFromExcelHandler(IExcelProductImportService excelProductImportService)
    {
        _excelProductImportService = excelProductImportService;
    }

    public async Task<Result<ProductId[], ErrorList>> Handle(ImportProductsFromExcelCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _excelProductImportService.ImportProductsFromExcelAsync(command.File,cancellationToken);

        if (result.IsFailure) return result.Error;

        return result.Value;
    }
}