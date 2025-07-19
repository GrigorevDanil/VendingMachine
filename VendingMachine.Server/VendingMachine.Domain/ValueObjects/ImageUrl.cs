using System.Net.Mime;
using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class ImageUrl : ValueObject
{
    public const int MAX_LENGTH = 2048;

    private static readonly string[] _allowedExtensions =
    [
        MediaTypeNames.Image.Png,
        MediaTypeNames.Image.Jpeg,
        MediaTypeNames.Image.Gif,
        "image/webp"
    ];

    private ImageUrl(string value) => Value = value;

    public string Value { get; }

    public static Result<ImageUrl, Error> Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(ImageUrl));

        if (!Uri.TryCreate(value, UriKind.Absolute, out var uri))
            return Errors.Url.InvalidUrlFormat();

        if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            return Errors.Url.InvalidUrlFormat();
        
        var extension = Path.GetExtension(uri.AbsolutePath).ToLowerInvariant();
        var mimeType = GetMimeType(extension);

        if (mimeType is null || !_allowedExtensions.Contains(mimeType))
            return Errors.File.ExtensionNotSupport();

        return new ImageUrl(value);
    }

    private static string? GetMimeType(string extension) => extension switch
    {
        ".png"  => MediaTypeNames.Image.Png,
        ".jpg"  => MediaTypeNames.Image.Jpeg,
        ".jpeg" => MediaTypeNames.Image.Jpeg,
        ".gif"  => MediaTypeNames.Image.Gif,
        ".webp" => "image/webp",
        _ => null
    };

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
}