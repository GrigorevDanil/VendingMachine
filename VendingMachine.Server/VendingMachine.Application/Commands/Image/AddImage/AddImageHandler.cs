﻿using CSharpFunctionalExtensions;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Commands.Image.AddImage;

public class AddImageHandler :  ICommandHandler<string,AddImageCommand>
{
    private readonly IImageService _imageService;

    public AddImageHandler(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task<Result<string, ErrorList>> Handle(AddImageCommand command, CancellationToken cancellationToken = default)
    {
       var addImageResult = await _imageService.AddImageAsync(command.File, cancellationToken);

       if (addImageResult.IsFailure)
           return addImageResult.Error;

       return addImageResult.Value;
    }
}