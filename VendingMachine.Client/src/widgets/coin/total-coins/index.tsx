import { coinListSlice } from "@/entities/coin";
import { useAppSelector } from "@/shared/model/redux";

export const TotalCoins = () => {
  const sumCoins = useAppSelector(coinListSlice.selectors.sumCoins);

  return (
    <div className="flex gap-20 items-center justify-end mt-6 text-right text-white font-bold text-xl ">
      <div className="flex gap-2 items-center justify-end">
        Итого
        <p className="text-2xl">{`${sumCoins} руб.`}</p>
      </div>
    </div>
  );
};
