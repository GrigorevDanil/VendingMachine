using VendingMachine.Application.Abstractions;

namespace VendingMachine.Application.Commands.SessionCommands.OccupySession;

public record OccupySessionCommand(Guid? IdSession) : ICommand;