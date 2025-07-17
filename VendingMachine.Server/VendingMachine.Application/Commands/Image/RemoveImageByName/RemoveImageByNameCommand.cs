using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Image.RemoveImageByName;

public record RemoveImageByNameCommand(string FileName) : ICommand;