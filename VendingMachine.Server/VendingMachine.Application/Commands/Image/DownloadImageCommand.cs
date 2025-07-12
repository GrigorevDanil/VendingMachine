using Microsoft.AspNetCore.Http;
using VendingMachine.Application.Abstractions;

namespace VendingMachine.Application.Commands.Image;

public record DownloadImageCommand(IFormFile File) : ICommand;