import { DepositCoin } from "@/entities/coin/types";
import { PaymentRequest } from "./paymentRequest";

export type PaymentQueryParams = {
  orderId: string;
  coins: DepositCoin[];
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
