using CSharpFunctionalExtensions;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Enums;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Commands.SessionCommands.OccupySession;

public class OccupySessionHandler : ICommandHandler<OccupySessionCommand>
{
    private readonly IBusyStateService _busyStateService;

    public OccupySessionHandler(IBusyStateService busyStateService)
    {
        _busyStateService = busyStateService;
    }


    public async Task<UnitResult<ErrorList>> Handle(OccupySessionCommand command, CancellationToken cancellationToken = default)
    {
        if (_busyStateService.State == BusyState.Open)
        {
            _busyStateService.ChangeBusy(BusyState.Close);
            _busyStateService.SetIdSession(command.IdSession);
        }

        return UnitResult.Success<ErrorList>();
    }
}