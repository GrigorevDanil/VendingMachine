"use client";

import { useAppSelector } from "@/shared/model/redux";
import { Panel } from "@/shared/ui/panel";
import { coinListSlice } from "../model/coin-list.slise";
import { CoinCard } from "./coin-card";
import { orderListSlice } from "@/entities/order";
import clsx from "clsx";

export const CoinList = () => {
  const coins = useAppSelector(coinListSlice.selectors.coins);
  const orderItems = useAppSelector(orderListSlice.selectors.orderItems);

  const sum = orderItems.reduce(
    (sum, item) => sum + item.product.price * item.quantity,
    0
  );

  const deposit = coins.reduce(
    (sum, item) => sum + item.denomination * item.quantity,
    0
  );

  const isDepositEnough = deposit >= sum;

  return (
    <Panel className="h-full">
      <div className="grid grid-cols-12 gap-4 mb-4 pr-2 font-bold text-white">
        <div className="col-span-6">Номинал</div>
        <div className="col-span-3 text-center">Количество</div>
        <div className="col-span-3 text-right">Сумма</div>
      </div>

      <div className="flex flex-col gap-4">
        {coins.map((coin, index) => (
          <CoinCard key={index} coin={coin} />
        ))}
      </div>

      <div className="flex gap-20 items-center justify-end mt-6 text-right text-white font-bold text-xl ">
        <div className="flex gap-2 items-center justify-end">
          {`Итоговая сумма: ${sum.toFixed(2)} руб.`}
        </div>
        <div className="flex gap-2 items-center justify-end">
          Вы внесли
          <p
            className={clsx(
              " text-2xl",
              isDepositEnough ? "text-green-500" : "text-red-500"
            )}
          >{`${deposit} руб.`}</p>
        </div>
      </div>
    </Panel>
  );
};
