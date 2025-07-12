using VendingMachine.Application.Commands.Image.RemoveImageByName;

namespace VendingMachine.API.Contracts.Image;

/// <summary>
/// Запрос на удаление фотографии из сервера
/// </summary>
/// <param name="FileName">Название файла (включая расширение)</param>
public record RemoveImageByNameRequest(string FileName)
{
    public RemoveImageByNameCommand ToCommand() => new(FileName);
}