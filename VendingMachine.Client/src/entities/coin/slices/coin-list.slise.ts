import { PayloadAction, createSlice } from "@reduxjs/toolkit/react";
import { Coin, Denomination } from "../model/coin";

export type State = {
  coins: Coin[];
};

const initialState: State = {
  coins: [
    {
      denomination: Denomination.One,
      stock: 0,
    },
    {
      denomination: Denomination.Two,
      stock: 0,
    },
    {
      denomination: Denomination.Five,
      stock: 0,
    },
    {
      denomination: Denomination.Ten,
      stock: 0,
    },
  ],
};

export const coinListSlice = createSlice({
  name: "coin-list",
  initialState,
  selectors: {
    coins: (state: State) => state.coins,
    coinsCount: (state: State) => state.coins.length,
  },
  reducers: {
    updateStock: (
      state,
      action: PayloadAction<{ denomination: Denomination; stock: number }>
    ) => {
      const { denomination, stock } = action.payload;
      const item = state.coins.find(
        (item) => item.denomination === denomination
      );
      if (item && stock > 0) {
        item.stock = stock;
      }
    },
    incrementStock: (
      state,
      action: PayloadAction<{ denomination: Denomination }>
    ) => {
      const { denomination } = action.payload;
      const item = state.coins.find(
        (item) => item.denomination === denomination
      );
      if (item) item.stock++;
    },
    decrementStock: (
      state,
      action: PayloadAction<{ denomination: Denomination }>
    ) => {
      const { denomination } = action.payload;
      const item = state.coins.find(
        (item) => item.denomination === denomination
      );
      if (item && item.stock > 0) item.stock--;
    },
  },
});
