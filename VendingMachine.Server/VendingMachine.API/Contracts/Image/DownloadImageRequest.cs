using VendingMachine.Application.Commands.Image;

namespace VendingMachine.API.Contracts.Image;

/// <summary>
/// Запрос для отправки изображения
/// </summary>
/// <param name="Image">Картинка</param>
public record DownloadImageRequest(IFormFile Image)
{
    public DownloadImageCommand ToCommand() => new (Image);
}