"use client";

import { BASE_URL } from "@/shared/api/constants";
import Image from "next/image";
import { OrderItem } from "../model/order";
import { QuantityCounter } from "@/features/quantity-counter";
import { RemoveProductFromOrder } from "@/features/remove-product-from-order";

export const OrderCard = ({ orderItem }: { orderItem: OrderItem }) => {
  return (
    <div className="grid grid-cols-12 gap-4 items-center bg-white bg-opacity-90 p-4 rounded-lg">
      <div className="col-span-6 flex items-center gap-4">
        <div className="w-20 h-20 relative">
          <Image
            src={`${BASE_URL}/images/${orderItem.product.filePath}`}
            alt={orderItem.product.title}
            className="object-contain rounded"
            fill
            sizes="80px"
          />
        </div>
        <p className="font-medium">{orderItem.product.title}</p>
      </div>

      <div className="col-span-3 ">
        <QuantityCounter orderItem={orderItem} />
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
