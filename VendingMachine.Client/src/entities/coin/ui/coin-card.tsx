"use client";

import { CoinImage } from "@/widgets/coin/coin-image";
import { DepositCoin } from "../types";
import { CoinCounter } from "@/features/coin/coin-counter";

export const CoinCard = ({ coin }: { coin: DepositCoin }) => {
  return (
    <div className="grid grid-cols-12 gap-4 items-center bg-white bg-opacity-90 p-4 rounded-lg">
      <div className="col-span-6 flex items-center gap-4">
        <CoinImage denomination={coin.denomination} />
        <p className="text-right font-medium">{coin.denomination} руб.</p>
      </div>

      <div className="col-span-3 ">
        <CoinCounter coin={coin} />
      </div>

      <div className="col-span-3 text-right font-medium">
        {coin.denomination * coin.quantity} руб.
      </div>
    </div>
  );
};
