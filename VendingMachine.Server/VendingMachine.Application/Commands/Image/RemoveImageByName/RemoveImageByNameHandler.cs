using CSharpFunctionalExtensions;
using VendingMachine.Application.Abstractions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Commands.Image.RemoveImageByName;

public class RemoveImageByNameHandler: ICommandHandler<RemoveImageByNameCommand>
{
    public async Task<UnitResult<ErrorList>> Handle(RemoveImageByNameCommand command, CancellationToken cancellationToken = default)
    {
        var imageFolder = Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
            "Images"
        );

        var filePath = Path.Combine(imageFolder, command.FileName);

        if (!File.Exists(filePath)) return Errors.File.NotFound(command.FileName).ToErrorList();
        
        File.Delete(filePath);

        return UnitResult.Success<ErrorList>();
    }
}