using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Services;

public class ImageService : IImageService
{
    public async Task<Result<string, ErrorList>> AddImageAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file.Length == 0) return Errors.General.ValueIsInvalid("File not provided").ToErrorList();

        if (!file.ContentType.StartsWith("image/")) return Errors.File.NotImage().ToErrorList();
        
        var imageFolder = Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
            "Images"
        );  
        
        if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);
        
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        
        var filePath = Path.Combine(imageFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        
        await file.CopyToAsync(stream, cancellationToken);

        return fileName;
    }

    public async Task<UnitResult<ErrorList>> RemoveImage(string fileName)
    {
        var imageFolder = Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
            "Images"
        );

        var filePath = Path.Combine(imageFolder, fileName);

        if (!File.Exists(filePath)) return Errors.File.NotFound(fileName).ToErrorList();
        
        File.Delete(filePath);

        return UnitResult.Success<ErrorList>();
    }
}