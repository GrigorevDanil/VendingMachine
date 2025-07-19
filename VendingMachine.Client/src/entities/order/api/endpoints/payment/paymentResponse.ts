import { CoinDtoSchema } from "@/entities/coin/api/dtos";
import { Coin } from "@/entities/coin/types";
import z from "zod";

export const PaymentResponseSchema = z.object({
  remains: z.number(),
  coins: z.array(CoinDtoSchema),
});

export type PaymentResponse = {
  remains: number;
  coins: Coin[];
};
