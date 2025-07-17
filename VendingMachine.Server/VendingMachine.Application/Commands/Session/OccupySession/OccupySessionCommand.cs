using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Commands.Session.OccupySession;

public record OccupySessionCommand(Guid? IdSession) : ICommand;