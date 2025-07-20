import { OrderItem } from "../../../types";

export const createOrderWithItemsQuery = (orderItems: OrderItem[]) => {
  const orderItemsData = orderItems.map((item) => ({
    productId: item.product.id,
    quantity: item.quantity,
  }));

  return {
    url: "api/order/multiple",
    method: "POST",
    body: JSON.stringify({
      orderItems: orderItemsData,
    }),
    headers: {
      "Content-Type": "application/json",
    },
  };
};
