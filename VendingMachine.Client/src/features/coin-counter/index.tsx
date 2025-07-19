import { coinListSlice } from "@/entities/coin";
import { Coin } from "@/entities/coin/model/coin";
import { useAppDispatch } from "@/shared/model/redux";
import { Counter } from "@/shared/ui/counter";

export const CoinCounter = ({ coin }: { coin: Coin }) => {
  const dispatch = useAppDispatch();

  const handleStockChange = (value: string) => {
    if (/^\d*$/.test(value)) {
      dispatch(
        coinListSlice.actions.updateQuantity({
          denomination: coin.denomination,
          quantity: parseInt(value),
        })
      );
    }
  };

  return (
    <Counter
      currentValue={coin.quantity}
      maxValue={999}
      minValue={0}
      onIncrement={() =>
        dispatch(
          coinListSlice.actions.incrementQuantity({
            denomination: coin.denomination,
          })
        )
      }
      onDecrement={() =>
        dispatch(
          coinListSlice.actions.decrementQuantity({
            denomination: coin.denomination,
          })
        )
      }
      onTextChange={(value) => handleStockChange(value)}
    />
  );
};
