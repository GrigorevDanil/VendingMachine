import { productListSlice } from "@/entities/product";
import { baseApi } from "@/shared/api";
import { configureStore } from "@reduxjs/toolkit/react";

export const makeStore = () =>
  configureStore({
    reducer: {
      [baseApi.reducerPath]: baseApi.reducer,
      [productListSlice.reducerPath]: productListSlice.reducer,
    },

    middleware: (getDefaultMiddleware) =>
      getDefaultMiddleware().concat(baseApi.middleware),
  });
