import {
  PayloadAction,
  createSelector,
  createSlice,
} from "@reduxjs/toolkit/react";
import { Coin } from "./coin";
import { PaymentResponse } from "@/entities/order/api/endpoints/payment/paymentResponse";

export type State = {
  remains: number;
  coins: Coin[];
};

const initialState: State = {
  remains: 0,
  coins: [
    {
      denomination: 1,
      quantity: 0,
    },
    {
      denomination: 2,
      quantity: 0,
    },
    {
      denomination: 5,
      quantity: 0,
    },
    {
      denomination: 10,
      quantity: 0,
    },
  ],
};

export const remainsListSlice = createSlice({
  name: "remains-list",
  initialState,
  selectors: {
    remains: (state: State) => state.remains,
    coins: (state: State) => state.coins,
    isRemains: createSelector(
      (state: State) => state.coins,
      (coins) => coins.some((coin) => coin.quantity > 0)
    ),
  },
  reducers: {
    setPaymentResult: (state, action: PayloadAction<PaymentResponse>) => {
      state.remains = action.payload.remains;
      state.coins = initialState.coins.map((coin) => {
        const newCoin = action.payload.coins.find(
          (c) => c.denomination === coin.denomination
        );
        return newCoin
          ? { ...coin, quantity: newCoin.quantity }
          : { ...coin, quantity: 0 };
      });
    },
    resetRemains: (state) => {
      state.remains = initialState.remains;
      state.coins = initialState.coins.map((coin) => ({ ...coin }));
    },
  },
});
