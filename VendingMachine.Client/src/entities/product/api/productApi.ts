import { baseApi } from "@/shared/api";
import {
  ProductDtoSchema,
  ProductPaginationList,
  ProductPaginationListSchema,
} from "./dtos";
import { Envelope, EnvelopeSchema } from "@/shared/model/envelope";
import { handleError } from "@/shared/lib/handleError";
import {
  GetProductsWithPaginationQueryParams,
  getProductsWithPaginationQuery,
} from "./endpoints/getProductsWithPagination/getProductsWithPaginationQuery";
import { importProductsFromExcelQuery } from "./endpoints/importProductsFromExcel/importProductsFromExcelQuery";
import {
  UpdateProductStockQueryParams,
  updateProductStockQuery,
} from "./endpoints/updateProductStock/updateProductStockQuery";

const productApi = baseApi.injectEndpoints({
  endpoints: (create) => ({
    getProductsWithPagination: create.query<
      Envelope<ProductPaginationList>,
      GetProductsWithPaginationQueryParams
    >({
      query: (params) => getProductsWithPaginationQuery(params),
      providesTags: ["Product"],
      transformResponse: (response: unknown): Envelope<ProductPaginationList> =>
        EnvelopeSchema(ProductPaginationListSchema(ProductDtoSchema)).parse(
          response
        ),
      transformErrorResponse: (response) => handleError(response),
    }),
    importProductFromExcel: create.mutation<void, File>({
      query: (file) => importProductsFromExcelQuery(file),
      transformErrorResponse: (response) => handleError(response),
      invalidatesTags: ["Product"],
    }),
    updateProductStock: create.mutation<void, UpdateProductStockQueryParams>({
      query: (params) => updateProductStockQuery(params),
      transformErrorResponse: (response) => handleError(response),
      invalidatesTags: ["Product"],
    }),
  }),
  overrideExisting: true,
});

export const {
  useGetProductsWithPaginationQuery,
  useImportProductFromExcelMutation,
  useUpdateProductStockMutation,
} = productApi;
