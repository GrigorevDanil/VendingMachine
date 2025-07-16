using VendingMachine.Application.Commands.SessionCommands.OccupySession;

namespace VendingMachine.API.Contracts.Session;

public record OccupySessionRequest(Guid? IdSession)
{
    public OccupySessionCommand ToCommand() => new(IdSession);
}