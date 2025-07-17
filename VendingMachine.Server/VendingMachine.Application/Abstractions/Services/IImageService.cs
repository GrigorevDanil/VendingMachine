using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Abstractions.Services;

public interface IImageService
{
    Task<Result<string, ErrorList>> AddImageAsync(IFormFile file, CancellationToken cancellationToken = default);
    
    Task<UnitResult<ErrorList>> RemoveImage(string fileName);
}