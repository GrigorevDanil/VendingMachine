import { Product } from "@/entities/product/types";

export type OrderId = string;

export type OrderItem = {
  product: Product;
  quantity: number;
};
