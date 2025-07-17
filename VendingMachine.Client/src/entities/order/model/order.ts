import { Product } from "@/entities/product/types";

export type OrderItem = {
  product: Product;
  quantity: number;
};
