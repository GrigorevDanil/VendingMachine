import { DepositCoin } from "@/entities/coin/types";

export const replenishBalanceQuery = (coins: DepositCoin[]) => {
  return {
    url: "api/coin",
    method: "POST",
    body: JSON.stringify({ coins: coins }),
    headers: {
      "Content-Type": "application/json",
    },
  };
};
