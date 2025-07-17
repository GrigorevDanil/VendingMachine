using Microsoft.AspNetCore.Http;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Image.AddImage;

public record AddImageCommand(IFormFile File) : ICommand;