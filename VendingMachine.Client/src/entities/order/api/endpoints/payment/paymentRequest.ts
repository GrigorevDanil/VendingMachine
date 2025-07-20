import { DepositCoin } from "@/entities/coin/types";

export type PaymentRequest = {
  orderId: string;
  coins: DepositCoin[];
};
