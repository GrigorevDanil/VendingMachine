import { productListSlice } from "@/entities/product";
import { orderListSlice } from "@/entities/order/slices/order-list.slice";
import { baseApi } from "@/shared/api";
import { configureStore } from "@reduxjs/toolkit/react";
import { coinListSlice } from "@/entities/coin/slices/coin-list.slise";

export const makeStore = () =>
  configureStore({
    reducer: {
      [baseApi.reducerPath]: baseApi.reducer,
      [productListSlice.reducerPath]: productListSlice.reducer,
      [orderListSlice.reducerPath]: orderListSlice.reducer,
      [coinListSlice.reducerPath]: coinListSlice.reducer,
    },

    middleware: (getDefaultMiddleware) =>
      getDefaultMiddleware().concat(baseApi.middleware),
  });
