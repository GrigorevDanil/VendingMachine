import z from "zod";

export const CoinDtoSchema = z.object({
  denomination: z.number(),
  stock: z.number(),
});
