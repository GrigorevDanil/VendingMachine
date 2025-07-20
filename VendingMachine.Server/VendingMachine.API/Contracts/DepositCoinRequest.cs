namespace VendingMachine.API.Contracts;

/// <summary>
/// Вносимая монета
/// </summary>
/// <param name="Denomination">Номинал</param>
/// <param name="Quantity">Количество</param>
public record DepositCoinRequest(int Denomination, int Quantity);