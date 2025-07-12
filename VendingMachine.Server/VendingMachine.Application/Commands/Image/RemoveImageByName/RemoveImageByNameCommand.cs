using VendingMachine.Application.Abstractions;

namespace VendingMachine.Application.Commands.Image.RemoveImageByName;

public record RemoveImageByNameCommand(string FileName) : ICommand;