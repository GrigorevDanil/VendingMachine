using CSharpFunctionalExtensions;
using VendingMachine.Application.Abstractions;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.ProductCommands.ImportProductsFromExcel;

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