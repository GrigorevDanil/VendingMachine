import { SortByProduct } from "@/entities/product/model/product-list.slice";

export type GetProductsWithPaginationQueryParams = {
  Page: number;
  PageSize: number;
  BrandId?: string | undefined;
  Title?: string | undefined;
  MinPrice?: number | undefined;
  SortBy?: SortByProduct;
  SortDirection?: "asc" | "desc" | undefined;
};

export const getProductsWithPaginationQuery = (
  params: GetProductsWithPaginationQueryParams
) => {
  const queryParams = new URLSearchParams();

  if (params.BrandId && params.BrandId !== "all")
    queryParams.append("BrandId", params.BrandId);
  if (params.Title) queryParams.append("Title", params.Title);
  if (params.MinPrice)
    queryParams.append("MinPrice", params.MinPrice.toString());
  if (params.SortBy) queryParams.append("SortBy", params.SortBy);
  if (params.SortDirection)
    queryParams.append("SortDirection", params.SortDirection);
  queryParams.append("Page", params.Page.toString());
  queryParams.append("PageSize", params.PageSize.toString());

  return `/api/product?${queryParams.toString()}`;
};
