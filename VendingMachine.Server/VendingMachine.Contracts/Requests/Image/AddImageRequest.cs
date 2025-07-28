using Microsoft.AspNetCore.Http;

namespace VendingMachine.Contracts.Requests.Image;

/// <summary>
/// Запрос для отправки изображения
/// </summary>
/// <param name="Image">Картинка</param>
public record AddImageRequest(IFormFile Image)
{
}