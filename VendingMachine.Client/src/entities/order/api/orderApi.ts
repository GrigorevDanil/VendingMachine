import { baseApi } from "@/shared/api";
import { OrderItem } from "../types";
import { createOrderWithItemsQuery } from "./endpoints/createOrderWithItems/createOrderWithItemsQuery";
import {
  CreateOrderWithItemsResponse,
  CreateOrderWithItemsResponseSchema,
} from "./endpoints/createOrderWithItems/createOrderWithItemsResponse";
import { Envelope, EnvelopeSchema } from "@/shared/model/envelope";
import { handleError } from "@/shared/lib/handleError";
import { paymentQuery } from "./endpoints/payment/paymentQuery";
import { PaymentRequest } from "./endpoints/payment/paymentRequest";
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
    payment: create.mutation<Envelope<PaymentResponse>, PaymentRequest>({
      query: (request) => paymentQuery(request),
      transformResponse: (response: unknown): Envelope<PaymentResponse> => {
        console.log(response);
        return EnvelopeSchema(PaymentResponseSchema).parse(response);
      },
      transformErrorResponse: (response) => handleError(response),
    }),
  }),
  overrideExisting: true,
});

export const { useCreateOrderWithItemsMutation, usePaymentMutation } = orderApi;
