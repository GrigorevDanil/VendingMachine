import { BrandId } from "@/entities/brand/model/brand";

export type ProductId = string;

export type ProductStatus = "available" | "selected" | "sold_out";

export type Product = {
  id: ProductId;
  filePath: string;
  title: string;
  price: number;
  stock: number;
  brandId: BrandId;
  status: ProductStatus;
};
