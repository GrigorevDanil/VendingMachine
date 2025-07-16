using Microsoft.AspNetCore.Http;
using VendingMachine.Application.Abstractions;

namespace VendingMachine.Application.Commands.ImageCommands.AddImage;

public record AddImageCommand(IFormFile File) : ICommand;