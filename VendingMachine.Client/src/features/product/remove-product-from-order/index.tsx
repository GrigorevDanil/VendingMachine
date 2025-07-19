import { orderListSlice } from "@/entities/order";
import { OrderItem } from "@/entities/order/model/order";
import { useAppDispatch } from "@/shared/model/redux";
import { Delete } from "@mui/icons-material";
import { IconButton } from "@mui/material";

export const RemoveProductFromOrder = ({
  orderItem,
}: {
  orderItem: OrderItem;
}) => {
  const dispatch = useAppDispatch();

  return (
    <IconButton
      color="error"
      onClick={() =>
        dispatch(
          orderListSlice.actions.removeProductFromOrder(orderItem.product.id)
        )
      }
      size="small"
      sx={{
        backgroundColor: "#ffeeee",
        "&:hover": {
          backgroundColor: "#ffdddd",
        },
      }}
    >
      <Delete fontSize="small" />
    </IconButton>
  );
};
