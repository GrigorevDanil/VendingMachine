using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Abstractions;

public interface IExcelProductImportService
{
    Task<Result<ProductId[],ErrorList>> ImportProductsFromExcelAsync(
        IFormFile file, 
        CancellationToken cancellationToken);
}