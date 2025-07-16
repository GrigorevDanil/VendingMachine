using VendingMachine.Application.Queries.Session.GetBusy;

namespace VendingMachine.API.Contracts.Session;

public record GetBusyRequest(Guid IdSession)
{
    public GetBusyQuery ToQuery() => new(IdSession);
}