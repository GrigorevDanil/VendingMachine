import { DepositCoinDtoSchema } from "@/entities/coin/api/dtos";
import { DepositCoin } from "@/entities/coin/types";
import z from "zod";

export const PaymentResponseSchema = z.object({
  remains: z.number(),
  coins: z.array(DepositCoinDtoSchema),
});

export type PaymentResponse = {
  remains: number;
  coins: DepositCoin[];
};
