using CSharpFunctionalExtensions;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Commands.Image.RemoveImageByName;

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