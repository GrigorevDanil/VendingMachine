"use client";

import { OrderList } from "@/entities/order/ui/order-list";
import { OrderActions } from "@/features/order/order-actions";
import { Header } from "@/widgets/header";

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
