using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Application.Enums;

namespace VendingMachine.Application.Services;

public class BusyStateService : IBusyStateService
{
    public BusyState State { get; set; }

    public void ChangeBusy(BusyState state) => State = state;

    public Guid? IdSession { get; set; }
    
    public void SetIdSession(Guid? idSession) => IdSession = idSession;
}