import { Coin } from "@/entities/coin/model/coin";
import { coinListSlice } from "@/entities/coin/slices/coin-list.slise";
import { useAppDispatch } from "@/shared/model/redux";
import { Counter } from "@/shared/ui/counter";

export const CoinCounter = ({ coin }: { coin: Coin }) => {
  const dispatch = useAppDispatch();

  const handleStockChange = (value: string) => {
    if (/^\d*$/.test(value)) {
      console.log(value);
      dispatch(
        coinListSlice.actions.updateStock({
          denomination: coin.denomination,
          stock: parseInt(value),
        })
      );
    }
  };

  return (
    <Counter
      currentValue={coin.stock}
      maxValue={999}
      minValue={0}
      onIncrement={() =>
        dispatch(
          coinListSlice.actions.incrementStock({
            denomination: coin.denomination,
          })
        )
      }
      onDecrement={() =>
        dispatch(
          coinListSlice.actions.decrementStock({
            denomination: coin.denomination,
          })
        )
      }
      onTextChange={(value) => handleStockChange(value)}
    />
  );
};
