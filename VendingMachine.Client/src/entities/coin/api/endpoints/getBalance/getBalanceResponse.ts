import z from "zod";
import { CoinDtoSchema } from "../../dtos";
import { Coin } from "@/entities/coin/model/coin";

export const GetBalanceResponseSchema = z.object({
  balance: z.number(),
  coins: z.array(CoinDtoSchema),
});

export type GetBalanceResponseResponse = {
  balance: number;
  coins: Coin[];
};
