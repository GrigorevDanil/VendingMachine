import { Coin } from "@/entities/coin/types";

export type PaymentRequest = {
  orderId: string;
  coins: Coin[];
};
