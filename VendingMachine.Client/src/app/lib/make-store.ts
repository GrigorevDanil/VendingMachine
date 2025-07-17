import { productListSlice } from "@/entities/product";
import { orderListSlice } from "@/entities/order/slices/order-list.slice";
import { baseApi } from "@/shared/api";
import { configureStore } from "@reduxjs/toolkit/react";

export const makeStore = () =>
  configureStore({
    reducer: {
      [baseApi.reducerPath]: baseApi.reducer,
      [productListSlice.reducerPath]: productListSlice.reducer,
      [orderListSlice.reducerPath]: orderListSlice.reducer,
    },

    middleware: (getDefaultMiddleware) =>
      getDefaultMiddleware().concat(baseApi.middleware),
  });
