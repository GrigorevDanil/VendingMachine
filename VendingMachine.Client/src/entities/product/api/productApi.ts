import { baseApi } from "@/shared/api";
import {
  ProductDtoSchema,
  ProductPaginationList,
  ProductPaginationListSchema,
} from "./dtos";
import { Envelope, EnvelopeSchema } from "@/shared/model/envelope";
import {
  GetProductsWithPaginationQueryParams,
  getProductsWithPaginationQuery,
} from "./queries";
import { handleError } from "@/shared/lib/handleError";

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
  }),
  overrideExisting: true,
});

export const { useGetProductsWithPaginationQuery } = productApi;
