import { OrderItem } from "@/entities/order/model/order";
import { orderListSlice } from "@/entities/order";
import { useAppDispatch } from "@/shared/model/redux";
import { Counter } from "@/shared/ui/counter";

export const OrderCounter = ({ orderItem }: { orderItem: OrderItem }) => {
  const dispatch = useAppDispatch();

  const handleQuantityChange = (value: string) => {
    if (/^\d*$/.test(value)) {
      console.log(value);
      dispatch(
        orderListSlice.actions.updateQuantity({
          productId: orderItem.product.id,
          quantity: parseInt(value),
        })
      );
    }
  };

  return (
    <Counter
      currentValue={orderItem.quantity}
      maxValue={orderItem.product.stock}
      onIncrement={() =>
        dispatch(
          orderListSlice.actions.incrementQuantity({
            productId: orderItem.product.id,
          })
        )
      }
      onDecrement={() =>
        dispatch(
          orderListSlice.actions.decrementQuantity({
            productId: orderItem.product.id,
          })
        )
      }
      onTextChange={(value) => handleQuantityChange(value)}
    />
  );
};
