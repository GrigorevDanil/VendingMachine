"use client";

import Image from "next/image";
import { Product } from "../types";
import { ProductCounter } from "@/features/product/product-counter";

export const ProductEditCard = ({ product }: { product: Product }) => {
  return (
    <div className="grid grid-cols-12 gap-4 items-center bg-white bg-opacity-90 p-4 rounded-lg">
      <div className="col-span-8 flex items-center gap-4">
        <div className="w-20 h-20 relative">
          <Image
            src={product.imageUrl}
            alt={product.title}
            className="object-contain rounded"
            fill
            sizes="80px"
          />
        </div>
        <p className="font-medium">{product.title}</p>
      </div>

      <div className="col-span-4 flex justify-end">
        <ProductCounter product={product} />
      </div>
    </div>
  );
};
