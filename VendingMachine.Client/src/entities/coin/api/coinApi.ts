import { baseApi } from "@/shared/api";
import { Envelope, EnvelopeSchema } from "@/shared/model/envelope";
import { handleError } from "@/shared/lib/handleError";
import {
  GetBalanceResponseResponse,
  GetBalanceResponseSchema,
} from "./endpoints/getBalance/getBalanceResponse";
import { replenishBalanceQuery } from "./endpoints/replenishBalance/replenishBalanceQuery";
import { DepositCoin } from "../types";

const coinApi = baseApi.injectEndpoints({
  endpoints: (create) => ({
    getBalance: create.query<Envelope<GetBalanceResponseResponse>, void>({
      query: () => "api/coin",
      providesTags: ["Coin"],
      transformResponse: (
        response: unknown
      ): Envelope<GetBalanceResponseResponse> =>
        EnvelopeSchema(GetBalanceResponseSchema).parse(response),
      transformErrorResponse: (response) => handleError(response),
    }),
    replenishBalance: create.mutation({
      query: (coins: DepositCoin[]) => replenishBalanceQuery(coins),
      transformErrorResponse: (response) => handleError(response),
      invalidatesTags: ["Coin"],
    }),
  }),
  overrideExisting: true,
});

export const { useGetBalanceQuery, useReplenishBalanceMutation } = coinApi;
