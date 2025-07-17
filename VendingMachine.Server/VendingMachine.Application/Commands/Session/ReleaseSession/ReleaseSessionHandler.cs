using CSharpFunctionalExtensions;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Application.Enums;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Commands.Session.ReleaseSession;

public class ReleaseSessionHandler : ICommandHandler<ReleaseSessionCommand>
{
    private readonly IBusyStateService _busyStateService;

    public ReleaseSessionHandler(IBusyStateService busyStateService)
    {
        _busyStateService = busyStateService;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(ReleaseSessionCommand command, CancellationToken cancellationToken = default)
    {
        if (_busyStateService.State == BusyState.Close)
        {
            _busyStateService.ChangeBusy(BusyState.Open);
            _busyStateService.SetIdSession(null);
        }
        
        return UnitResult.Success<ErrorList>();
    }
}