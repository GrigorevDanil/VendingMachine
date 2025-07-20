import { baseApi } from "@/shared/api";
import { Brand } from "../types";
import { BrandDtoSchema } from "./dtos";
import { Envelope, EnvelopeSchema } from "@/shared/model/envelope";
import { handleError } from "@/shared/lib/handleError";

const brandApi = baseApi.injectEndpoints({
  endpoints: (create) => ({
    getBrands: create.query<Envelope<Brand[]>, void>({
      query: () => "/api/brand",
      providesTags: ["Brand"],
      transformResponse: (response: unknown): Envelope<Brand[]> =>
        EnvelopeSchema(BrandDtoSchema).parse(response),
      transformErrorResponse: (response: unknown) => handleError(response),
    }),
  }),
  overrideExisting: true,
});

export const { useGetBrandsQuery } = brandApi;
