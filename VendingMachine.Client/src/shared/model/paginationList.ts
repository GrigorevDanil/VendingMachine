import z from "zod";

export const PaginationListSchema = <T extends z.ZodTypeAny>(itemSchema: T) =>
  z.object({
    items: z.array(itemSchema),
    totalCount: z.number(),
    pageSize: z.number(),
    page: z.number(),
    pageCount: z.number(),
    hasNextPage: z.boolean(),
    hasPreviousPage: z.boolean(),
    options: z.record(z.string(), z.unknown()).optional(),
  });

export type PaginationList<T> = {
  items: T[];
  totalCount: number;
  pageSize: number;
  page: number;
  pageCount: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  options?: Record<string, unknown>;
};
