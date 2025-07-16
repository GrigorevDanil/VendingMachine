import Image from "next/image";
import { BASE_URL } from "@/shared/api/constants";
import { Card, CardActions } from "@mui/material";
import { ToggleProduct } from "@/features/toggle-product";
import { Product } from "../types";

export const ProductCard = ({ product }: { product: Product }) => {
  return (
    <Card className="bg-white shadow-md rounded-lg overflow-hidden h-full flex flex-col">
      <div className="relative w-full aspect-square">
        <Image
          src={`${BASE_URL}/images/${product.filePath}`}
          alt={product.title}
          className="object-contain bg-gray-50"
          fill
          sizes="(max-width: 640px) 100vw, (max-width: 768px) 50vw, (max-width: 1024px) 33vw, 25vw"
          priority
        />
      </div>

      <div className="flex flex-col flex-grow">
        <div className="bg-orange-950 p-4 w-full flex flex-col rounded flex-1">
          <h3 className="text-white font-medium text-lg mb-2 line-clamp-2">
            {product.title}
          </h3>
          <p className="text-orange-200 text-xl font-bold mt-auto">
            {product.price} руб.
          </p>
        </div>

        <CardActions>
          <ToggleProduct product={product} />
        </CardActions>
      </div>
    </Card>
  );
};
