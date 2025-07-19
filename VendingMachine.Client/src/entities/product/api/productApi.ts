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
    importProductFromExcel: create.mutation<void, FormData>({
      query: (formData) => ({
        url: "api/product/import",
        method: "POST",
        body: formData,
      }),
    }),
  }),
  overrideExisting: true,
});

export const {
  useGetProductsWithPaginationQuery,
  useImportProductFromExcelMutation,
} = productApi;
