using VendingMachine.Application.Enums;

namespace VendingMachine.Application.Abstractions.Services;

public interface IBusyStateService
{
    BusyState State { get; set; }
    
    void ChangeBusy(BusyState state);
    
    Guid? IdSession { get; set; }
    
    void SetIdSession(Guid? idSession);
}