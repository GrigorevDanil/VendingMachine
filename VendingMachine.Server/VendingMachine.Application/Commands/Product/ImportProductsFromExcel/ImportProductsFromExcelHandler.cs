using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Application.Commands.Order.Payment;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Product.ImportProductsFromExcel;

public class ImportProductsFromExcelHandler : ICommandHandler<ProductId[],ImportProductsFromExcelCommand>
{
    private readonly IExcelProductImportService _excelProductImportService;
    
    private readonly ILogger<ImportProductsFromExcelHandler> _logger;

    public ImportProductsFromExcelHandler(IExcelProductImportService excelProductImportService, ILogger<ImportProductsFromExcelHandler> logger)
    {
        _excelProductImportService = excelProductImportService;
        _logger = logger;
    }

    public async Task<Result<ProductId[], ErrorList>> Handle(ImportProductsFromExcelCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _excelProductImportService.ImportProductsFromExcelAsync(command.File,cancellationToken);

        if (result.IsFailure) 
            return result.Error;
        
        _logger.LogInformation("Products was being imported successfully");

        return result.Value;
    }
}