import { coinListSlice } from "@/entities/coin";
import { remainsListSlice } from "@/entities/coin/model/slices/remains-list.slice";
import { orderListSlice } from "@/entities/order";
import { productListSlice } from "@/entities/product";
import { baseApi } from "@/shared/api";
import { configureStore } from "@reduxjs/toolkit/react";

export const makeStore = () =>
  configureStore({
    reducer: {
      [baseApi.reducerPath]: baseApi.reducer,
      [productListSlice.reducerPath]: productListSlice.reducer,
      [orderListSlice.reducerPath]: orderListSlice.reducer,
      [coinListSlice.reducerPath]: coinListSlice.reducer,
      [remainsListSlice.reducerPath]: remainsListSlice.reducer,
    },

    middleware: (getDefaultMiddleware) =>
      getDefaultMiddleware().concat(baseApi.middleware),
  });
