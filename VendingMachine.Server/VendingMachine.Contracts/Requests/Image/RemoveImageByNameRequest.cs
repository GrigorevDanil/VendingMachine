namespace VendingMachine.Contracts.Requests.Image;

/// <summary>
/// Запрос на удаление фотографии из сервера
/// </summary>
/// <param name="FileName">Название файла (включая расширение)</param>
public record RemoveImageByNameRequest(string FileName)
{
}