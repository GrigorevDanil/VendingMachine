"use client";

import { useAppSelector } from "@/shared/model/redux";
import { orderListSlice } from "../model/order-list.slice";
import { Panel } from "@/shared/ui/panel";
import { OrderCard } from "./order-card";

export const OrderList = () => {
  const orderItems = useAppSelector(orderListSlice.selectors.orderItems);
  const sumOrder = useAppSelector(orderListSlice.selectors.sumOrder);

  if (orderItems.length === 0)
    return (
      <Panel>
        <p className="text-4xl text-white m-auto">
          У вас нет ни одного товара, вернитесь на страница каталога
        </p>
      </Panel>
    );

  return (
    <Panel className="h-full">
      <div className="grid grid-cols-12 gap-4 mb-4 font-bold text-white">
        <div className="col-span-6">Товар</div>
        <div className="col-span-3 text-center">Количество</div>
        <div className="col-span-2 text-center">Цена</div>
        <div className="col-span-1"></div>
      </div>

      <div className="flex flex-col gap-4">
        {orderItems.map((orderItem, index) => (
          <OrderCard key={index} orderItem={orderItem} />
        ))}
      </div>

      <div className="mt-6 text-right text-white font-bold text-xl">
        {`Итоговая сумма: ${sumOrder.toFixed(2)} руб.`}
      </div>
    </Panel>
  );
};
