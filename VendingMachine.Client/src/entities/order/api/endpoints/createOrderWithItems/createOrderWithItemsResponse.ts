import z from "zod";

export const CreateOrderWithItemsResponseSchema = z.object({
  orderId: z.uuid(),
  orderItemIds: z.array(z.uuid()),
});

export type CreateOrderWithItemsResponse = {
  orderId: string;
  orderItemIds: string[];
};
