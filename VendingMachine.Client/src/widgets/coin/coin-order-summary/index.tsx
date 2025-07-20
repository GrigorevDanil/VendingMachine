import { coinListSlice } from "@/entities/coin";
import { orderListSlice } from "@/entities/order";
import { useAppSelector } from "@/shared/model/redux";
import clsx from "clsx";

export const CoinOrderSummary = () => {
  const sumCoins = useAppSelector(coinListSlice.selectors.sumCoins);
  const sumOrder = useAppSelector(orderListSlice.selectors.sumOrder);

  const isDepositEnough = sumCoins >= sumOrder;

  return (
    <div className="flex gap-20 items-center justify-end mt-6 text-right text-white font-bold text-xl ">
      <div className="flex gap-2 items-center justify-end">
        {`Итоговая сумма: ${sumOrder.toFixed(2)} руб.`}
      </div>
      <div className="flex gap-2 items-center justify-end">
        Вы внесли
        <p
          className={clsx(
            " text-2xl",
            isDepositEnough ? "text-green-500" : "text-red-500"
          )}
        >{`${sumCoins} руб.`}</p>
      </div>
    </div>
  );
};
