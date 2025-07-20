import { DepositCoin } from "@/entities/coin/types";

export type PaymentQueryParams = {
  orderId: string;
  coins: DepositCoin[];
};

export const paymentQuery = (request: PaymentQueryParams) => {
  return {
    url: "api/order/payment/" + request.orderId,
    method: "POST",
    body: JSON.stringify({ coins: request.coins }),
    headers: {
      "Content-Type": "application/json",
    },
  };
};
