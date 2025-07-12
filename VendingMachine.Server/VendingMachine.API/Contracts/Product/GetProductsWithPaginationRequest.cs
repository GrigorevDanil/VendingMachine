using VendingMachine.Application.Queries.Product.GetProductsWithPagination;

namespace VendingMachine.API.Contracts.Product;

/// <summary>
/// Запрос для получения списка товаров (напитков) с пагинацией и фильтрами
/// </summary>
/// <param name="BrandId">Идентификатор бренда</param>
/// <param name="Title">Название товара</param>
/// <param name="MinPrice">Минимальная стоимость</param>
/// <param name="SortBy">Сортировать по полю (title/price)</param>
/// <param name="SortDirection">Порядок сортировки (asc/desc) (По умолчанию - asc)</param>
/// <param name="Page">Текущая страница</param>
/// <param name="PageSize">Количество элементов в странице</param>
public record GetProductsWithPaginationRequest(
    Guid? BrandId,
    string? Title,
    decimal? MinPrice,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetProductWithPaginationQuery ToQuery() =>
        new(BrandId, Title,MinPrice, SortBy, SortDirection, Page, PageSize);
}