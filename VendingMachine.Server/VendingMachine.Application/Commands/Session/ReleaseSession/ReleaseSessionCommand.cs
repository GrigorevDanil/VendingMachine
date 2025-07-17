using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Session.ReleaseSession;

public record ReleaseSessionCommand() : ICommand;