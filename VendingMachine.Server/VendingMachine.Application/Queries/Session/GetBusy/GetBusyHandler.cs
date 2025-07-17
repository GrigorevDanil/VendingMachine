

using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Application.Enums;

namespace VendingMachine.Application.Queries.Session.GetBusy;

public class GetBusyHandler : IQueryHandler<string, GetBusyQuery>
{
    private readonly IBusyStateService _busyStateService;

    public GetBusyHandler(IBusyStateService busyStateService)
    {
        _busyStateService = busyStateService;
    }
    

    public async Task<string> Handle(GetBusyQuery query, CancellationToken cancellationToken = default)
    {
        if (_busyStateService.State == BusyState.Close)
        {
            if (query.IdSession == _busyStateService.IdSession) return nameof(BusyState.Available);
            return nameof(BusyState.Close);
        }

        return nameof(BusyState.Open);
    }
}