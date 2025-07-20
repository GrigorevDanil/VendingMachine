"use client";

import { useAppSelector } from "@/shared/model/redux";
import { Panel } from "@/shared/ui/panel";
import { ReactNode } from "react";
import { coinListSlice } from "../../../entities/coin/model/slices/coin-list.slise";
import { CoinCard } from "../../../entities/coin/ui/coin-card";

type Props = {
  footer?: ReactNode;
};

export const CoinList = ({ footer }: Props) => {
  const coins = useAppSelector(coinListSlice.selectors.coins);

  return (
    <Panel className="h-full">
      <div className="grid grid-cols-12 gap-4 mb-4 pr-2 font-bold text-white">
        <div className="col-span-6">Номинал</div>
        <div className="col-span-3 text-center">Количество</div>
        <div className="col-span-3 text-right">Сумма</div>
      </div>

      <div className="flex flex-col gap-4">
        {coins.map((coin) => (
          <CoinCard key={coin.denomination} coin={coin} />
        ))}
      </div>
      {footer}
    </Panel>
  );
};
