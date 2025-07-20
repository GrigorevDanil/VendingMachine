"use client";

import { OrderActions } from "@/features/order/order-actions";
import { Header } from "@/widgets/header";
import { OrderList } from "@/widgets/order/order-list";

export const OrderPage = () => {
  return (
    <div className="flex flex-col gap-2 h-full">
      <Header>
        <p> - Оформление заказа</p>
      </Header>
      <OrderList />
      <OrderActions />
    </div>
  );
};
