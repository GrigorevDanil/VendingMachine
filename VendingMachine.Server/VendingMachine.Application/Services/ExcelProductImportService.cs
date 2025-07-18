using ClosedXML.Excel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Services;

public class ExcelProductImportService : IExcelProductImportService
{
    private readonly IRepository<Product, ProductId> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExcelProductImportService(
        IRepository<Product, ProductId> productRepository, 
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProductId[], ErrorList>> ImportProductsFromExcelAsync(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file.Length == 0)
            return Errors.File.NotProvide().ToErrorList();

        if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            return Errors.File.NotExcel().ToErrorList();

        const string TITLE_NAME = nameof(Title);
        
        const string PRICE_NAME = nameof(Price);
        
        const string FILEPATH_NAME = nameof(FilePath);
        
        const string STOCK_NAME = nameof(Stock);
        
        const string BRAND_ID_NAME = nameof(BrandId);

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);
        stream.Position = 0;

        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);
        var headerRow = worksheet.FirstRowUsed();
        var headers = headerRow.CellsUsed()
            .ToDictionary(c => c.Value.ToString().Trim(), c => c.Address.ColumnNumber);
        
        var requiredHeaders = new[] { TITLE_NAME,  PRICE_NAME, FILEPATH_NAME, STOCK_NAME, BRAND_ID_NAME };
        foreach (var header in requiredHeaders)
        {
            if (!headers.ContainsKey(header))
                return Errors.File.FileIsInvalid($"Missing required column: {header}").ToErrorList();
        }

        var productIds = new List<ProductId>();
        var dataRange = worksheet.Range(
            headerRow.RowNumber() + 1, 
            1, 
            worksheet.LastRowUsed().RowNumber(), 
            worksheet.LastColumnUsed().ColumnNumber());

        foreach (var row in dataRange.Rows())
        {
            var title = row.Cell(headers[TITLE_NAME]).GetString();
            var price = row.Cell(headers[PRICE_NAME]).GetValue<decimal>();
            var stockIsInt = int.TryParse(row.Cell(headers[STOCK_NAME]).GetString(), out var stock);
            var imagePath = row.Cell(headers[FILEPATH_NAME]).GetString();
            var brandIdIsGuid = Guid.TryParse(row.Cell(headers[BRAND_ID_NAME]).GetString(), out var brandId);

            if (!stockIsInt) 
                return Errors.General.ValueIsInvalid(STOCK_NAME).ToErrorList();
            
            if (!brandIdIsGuid) 
                return Errors.General.ValueIsInvalid(BRAND_ID_NAME).ToErrorList();

            var titleResult = Title.Of(title);
            if (titleResult.IsFailure)
                return titleResult.Error.ToErrorList();

            var priceResult = Price.Of(price);
            if (priceResult.IsFailure)
                return priceResult.Error.ToErrorList();

            var stockResult = Stock.Of(stock);
            if (stockResult.IsFailure)
                return stockResult.Error.ToErrorList();

            var filePathResult = FilePath.Of(imagePath);
            if (filePathResult.IsFailure)
                return filePathResult.Error.ToErrorList();

            var productId = ProductId.Create();
            var product = new Product(
                productId,
                filePathResult.Value,
                titleResult.Value,
                priceResult.Value,
                stockResult.Value,
                BrandId.Of(brandId));

            productIds.Add(await _productRepository.AddAsync(product, cancellationToken));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return productIds.ToArray();
    }
}