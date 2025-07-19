import { Coin } from "@/entities/coin/types";
import { PaymentRequest } from "./paymentRequest";

export type PaymentQueryParams = {
  orderId: string;
  coins: Coin[];
};

export const paymentQuery = (request: PaymentRequest) => {
  return {
    url: "api/order/payment/" + request.orderId,
    method: "POST",
    body: JSON.stringify({ coins: request.coins }),
    headers: {
      "Content-Type": "application/json",
    },
  };
};
