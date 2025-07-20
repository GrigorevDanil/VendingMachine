import z from "zod";

export const DepositCoinDtoSchema = z.object({
  denomination: z.number(),
  quantity: z.number(),
});
