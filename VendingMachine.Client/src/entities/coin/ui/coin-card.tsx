"use client";

import { Coin, getDenomination } from "../model/coin";
import { CoinCounter } from "@/features/coin-counter";

export const CoinCard = ({ coin }: { coin: Coin }) => {
  return (
    <div className="grid grid-cols-12 gap-4 items-center bg-white bg-opacity-90 p-4 rounded-lg">
      <div className="col-span-6 flex items-center gap-4">
        <div className="w-20 h-20 relative flex items-center justify-center border-4 border-black rounded-full bg-gray-500 text-white text-4xl opacity-80">
          {getDenomination(coin)}
        </div>
        <p className="font-medium">{getDenomination(coin) + " руб."}</p>
      </div>

      <div className="col-span-3 ">
        <CoinCounter coin={coin} />
      </div>

      <div className="col-span-3 text-right font-medium">
        {getDenomination(coin) * coin.stock} руб.
      </div>
    </div>
  );
};
