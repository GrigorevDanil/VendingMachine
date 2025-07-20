import z from "zod";
import { ProductStatus } from "../../types";

export const ProductDtoSchema = z
  .object({
    id: z.uuid(),
    imageUrl: z.string(),
    title: z.string(),
    price: z.number(),
    stock: z.number(),
    brandId: z.uuid(),
  })
  .transform((product) => ({
    ...product,
    status: (product.stock === 0 ? "sold_out" : "available") as ProductStatus,
  }));
