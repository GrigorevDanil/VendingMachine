export type UpdateProductStockQueryParams = {
  productId: string;
  stock: number;
};

export const updateProductStockQuery = (
  params: UpdateProductStockQueryParams
) => {
  const formData = new FormData();

  formData.append("Stock", params.stock.toString());

  return {
    url: "api/product/" + params.productId,
    method: "PUT",
    body: formData,
  };
};
