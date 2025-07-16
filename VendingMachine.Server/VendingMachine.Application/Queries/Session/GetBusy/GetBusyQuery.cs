using VendingMachine.Application.Abstractions;

namespace VendingMachine.Application.Queries.Session.GetBusy;

public record GetBusyQuery(Guid IdSession) : IQuery;