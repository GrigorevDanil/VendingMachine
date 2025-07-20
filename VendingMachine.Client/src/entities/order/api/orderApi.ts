import { baseApi } from "@/shared/api";
import { OrderItem } from "../types";
import { createOrderWithItemsQuery } from "./endpoints/createOrderWithItems/createOrderWithItemsQuery";
import {
  CreateOrderWithItemsResponse,
  CreateOrderWithItemsResponseSchema,
} from "./endpoints/createOrderWithItems/createOrderWithItemsResponse";
import { Envelope, EnvelopeSchema } from "@/shared/model/envelope";
import { handleError } from "@/shared/lib/handleError";
import {
  PaymentQueryParams,
  paymentQuery,
} from "./endpoints/payment/paymentQuery";
import {
  PaymentResponse,
  PaymentResponseSchema,
} from "./endpoints/payment/paymentResponse";

const orderApi = baseApi.injectEndpoints({
  endpoints: (create) => ({
    createOrderWithItems: create.mutation<
      Envelope<CreateOrderWithItemsResponse>,
      OrderItem[]
    >({
      query: (orderItems) => createOrderWithItemsQuery(orderItems),
      transformResponse: (
        response: unknown
      ): Envelope<CreateOrderWithItemsResponse> =>
        EnvelopeSchema(CreateOrderWithItemsResponseSchema).parse(response),
      transformErrorResponse: (response) => handleError(response),
      invalidatesTags: ["Product"],
    }),
    payment: create.mutation<Envelope<PaymentResponse>, PaymentQueryParams>({
      query: (request) => paymentQuery(request),
      transformResponse: (response: unknown): Envelope<PaymentResponse> =>
        EnvelopeSchema(PaymentResponseSchema).parse(response),
      transformErrorResponse: (response) => handleError(response),
    }),
  }),
  overrideExisting: true,
});

export const { useCreateOrderWithItemsMutation, usePaymentMutation } = orderApi;
