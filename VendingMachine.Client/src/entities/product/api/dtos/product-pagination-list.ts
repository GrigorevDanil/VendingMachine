import z from "zod";
import { Product } from "../../types";
import {
  PaginationList,
  PaginationListSchema,
} from "@/shared/model/paginationList";

export const ProductPaginationListSchema = <T extends z.ZodTypeAny>(
  itemSchema: T
) =>
  PaginationListSchema(itemSchema).extend({
    options: z
      .object({
        maxPrice: z.number(),
      })
      .optional(),
  });

export type ProductPaginationList<T = Product> = PaginationList<T> & {
  options?: {
    maxPrice: number;
  };
};
