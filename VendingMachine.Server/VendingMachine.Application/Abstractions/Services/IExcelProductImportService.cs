using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Abstractions.Services;

public interface IExcelProductImportService
{
    Task<Result<ProductId[],ErrorList>> ImportProductsFromExcelAsync(
        IFormFile file, 
        CancellationToken cancellationToken);
}