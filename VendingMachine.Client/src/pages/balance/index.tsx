"use client";

import { CoinBalance } from "@/widgets/coin/coin-balance";
import { TotalCoins } from "@/widgets/coin/total-coins";
import { BalanceActions } from "@/features/coin/balance-actions";
import { CoinList } from "@/widgets/coin/coin-list";

export const BalancePage = () => {
  return (
    <div className="flex flex-col gap-2">
      <CoinBalance />
      <CoinList footer={<TotalCoins />} />
      <BalanceActions />
    </div>
  );
};
