using CSharpFunctionalExtensions;
using VendingMachine.Application.Abstractions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Commands.ImageCommands.RemoveImageByName;

public class RemoveImageByNameHandler: ICommandHandler<RemoveImageByNameCommand>
{
    private readonly IImageService _imageService;

    public RemoveImageByNameHandler(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task<UnitResult<ErrorList>> Handle(RemoveImageByNameCommand command, CancellationToken cancellationToken = default)
    {
        return await _imageService.RemoveImage(command.FileName);
    }
}