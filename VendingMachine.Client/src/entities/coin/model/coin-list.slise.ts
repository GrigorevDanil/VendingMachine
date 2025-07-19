import {
  PayloadAction,
  createSelector,
  createSlice,
} from "@reduxjs/toolkit/react";
import { Coin } from "./coin";

export type State = {
  coins: Coin[];
};

const initialState: State = {
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

export const coinListSlice = createSlice({
  name: "coin-list",
  initialState,
  selectors: {
    coins: (state: State) => state.coins,
    coinsCount: (state: State) => state.coins.length,
    addedCoins: createSelector(
      (state: State) => state.coins,
      (coins) => coins.filter((coin) => coin.quantity > 0)
    ),
    sumCoins: createSelector(
      (state: State) => state.coins,
      (coins) =>
        coins.reduce((sum, item) => sum + item.denomination * item.quantity, 0)
    ),
  },
  reducers: {
    updateQuantity: (
      state,
      action: PayloadAction<{ denomination: number; quantity: number }>
    ) => {
      const { denomination, quantity: stock } = action.payload;
      const item = state.coins.find(
        (item) => item.denomination === denomination
      );
      if (item && stock > 0) {
        item.quantity = stock;
      }
    },
    incrementQuantity: (
      state,
      action: PayloadAction<{ denomination: number }>
    ) => {
      const { denomination } = action.payload;
      const item = state.coins.find(
        (item) => item.denomination === denomination
      );
      if (item) item.quantity++;
    },
    decrementQuantity: (
      state,
      action: PayloadAction<{ denomination: number }>
    ) => {
      const { denomination } = action.payload;
      const item = state.coins.find(
        (item) => item.denomination === denomination
      );
      if (item && item.quantity > 0) item.quantity--;
    },
    resetCoins: (state) => {
      state.coins = initialState.coins;
    },
  },
});
