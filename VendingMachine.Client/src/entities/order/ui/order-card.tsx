"use client";

import Image from "next/image";
import { OrderItem } from "../model/order";
import { RemoveProductFromOrder } from "@/features/product/remove-product-from-order";
import { OrderCounter } from "@/features/order/order-counter";

export const OrderCard = ({ orderItem }: { orderItem: OrderItem }) => {
  return (
    <div className="grid grid-cols-12 gap-4 items-center bg-white bg-opacity-90 p-4 rounded-lg">
      <div className="col-span-6 flex items-center gap-4">
        <div className="w-20 h-20 relative">
          <Image
            src={orderItem.product.imageUrl}
            alt={orderItem.product.title}
            className="object-contain rounded"
            fill
            sizes="80px"
          />
        </div>
        <p className="font-medium">{orderItem.product.title}</p>
      </div>

      <div className="col-span-3 ">
        <OrderCounter orderItem={orderItem} />
      </div>

      <div className="col-span-2 text-center font-medium">
        {(orderItem.product.price * orderItem.quantity).toFixed(2)} руб.
      </div>

      <div className="w-fit flex justify-end">
        <RemoveProductFromOrder orderItem={orderItem} />
      </div>
    </div>
  );
};
