using VendingMachine.Application.Commands.Image.AddImage;

namespace VendingMachine.API.Contracts.Image;

/// <summary>
/// Запрос для отправки изображения
/// </summary>
/// <param name="Image">Картинка</param>
public record AddImageRequest(IFormFile Image)
{
    public AddImageCommand ToCommand() => new (Image);
}