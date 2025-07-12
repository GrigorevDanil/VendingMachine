using CSharpFunctionalExtensions;
using VendingMachine.Application.Abstractions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Commands.Image.AddImage;

public class AddImageHandler :  ICommandHandler<string,AddImageCommand>
{
    public async Task<Result<string, ErrorList>> Handle(AddImageCommand command, CancellationToken cancellationToken = default)
    {
        if (command.File.Length == 0) return Errors.General.ValueIsInvalid("File not provided").ToErrorList();

        if (!command.File.ContentType.StartsWith("image/")) return Errors.File.NotImage().ToErrorList();
        
        var imageFolder = Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
            "Images"
        );  
        
        if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);
        
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(command.File.FileName)}";
        
        var filePath = Path.Combine(imageFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        
        await command.File.CopyToAsync(stream, cancellationToken);

        return fileName;
    }
}