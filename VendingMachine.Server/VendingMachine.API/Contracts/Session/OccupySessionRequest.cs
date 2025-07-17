using VendingMachine.Application.Commands.Session.OccupySession;

namespace VendingMachine.API.Contracts.Session;

public record OccupySessionRequest(Guid? IdSession)
{
    public OccupySessionCommand ToCommand() => new(IdSession);
}