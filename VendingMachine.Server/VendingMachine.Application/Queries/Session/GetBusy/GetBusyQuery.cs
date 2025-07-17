using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Queries.Session.GetBusy;

public record GetBusyQuery(Guid IdSession) : IQuery;