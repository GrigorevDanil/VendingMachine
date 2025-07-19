namespace VendingMachine.API.State;

public static class SiteLock
{
    public static bool IsBusy { get; set; } = false;
}