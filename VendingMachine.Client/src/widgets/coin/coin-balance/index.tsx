"use client";

import { CountCoinCard } from "@/entities/coin";
import { useGetBalanceQuery } from "@/entities/coin/api/coinApi";
import { Panel } from "@/shared/ui/panel";
import { CircularProgress } from "@mui/material";

export const CoinBalance = () => {
  const { data: getBalanceResult, isLoading } = useGetBalanceQuery();

  const sortedCoins = getBalanceResult?.result?.coins
    ? [...getBalanceResult.result.coins].sort(
        (a, b) => a.denomination - b.denomination
      )
    : [];

  return (
    <Panel className="h-full">
      {isLoading ? (
        <div className="flex">
          <CircularProgress className="text-white m-auto" />
        </div>
      ) : (
        <div className="flex flex-col gap-4">
          <p className="text-4xl text-white">
            Баланс: {getBalanceResult?.result?.balance} руб.
          </p>
          <div className="flex justify-between flex-wrap gap-2">
            {sortedCoins.map((coin) => (
              <CountCoinCard key={coin.denomination} coin={coin} />
            ))}
          </div>
        </div>
      )}
    </Panel>
  );
};
