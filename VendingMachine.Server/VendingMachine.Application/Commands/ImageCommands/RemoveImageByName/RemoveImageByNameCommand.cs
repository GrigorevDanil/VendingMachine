using VendingMachine.Application.Abstractions;

namespace VendingMachine.Application.Commands.ImageCommands.RemoveImageByName;

public record RemoveImageByNameCommand(string FileName) : ICommand;