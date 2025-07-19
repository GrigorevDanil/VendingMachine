import {
  PayloadAction,
  createSelector,
  createSlice,
} from "@reduxjs/toolkit/react";
import { OrderItem } from "./order";
import { Product } from "@/entities/product/types";

export type State = {
  orderItems: OrderItem[];
};

const initialState: State = {
  orderItems: [],
};

export const orderListSlice = createSlice({
  name: "order-list",
  initialState,
  selectors: {
    orderItems: (state: State) => state.orderItems,
    isProductInOrder: (state: State, productId: string) =>
      state.orderItems.some((item) => item.product.id === productId),
    orderItemsCount: (state: State) => state.orderItems.length,
    sumOrder: createSelector(
      (state: State) => state.orderItems,
      (items) =>
        items.reduce((sum, item) => sum + item.product.price * item.quantity, 0)
    ),
  },
  reducers: {
    addProductInOrder: (state, action: PayloadAction<{ product: Product }>) => {
      const { product } = action.payload;
      state.orderItems.push({ product, quantity: 1 });
    },
    removeProductFromOrder: (state, action: PayloadAction<string>) => {
      const productId = action.payload;
      state.orderItems = state.orderItems.filter(
        (item) => item.product.id !== productId
      );
    },
    updateQuantity: (
      state,
      action: PayloadAction<{ productId: string; quantity: number }>
    ) => {
      const { productId, quantity } = action.payload;
      const item = state.orderItems.find(
        (item) => item.product.id === productId
      );
      if (item && quantity > 0) {
        if (item.product.stock < quantity) item.quantity = item.product.stock;
        else item.quantity = quantity;
      }
    },
    incrementQuantity: (
      state,
      action: PayloadAction<{ productId: string }>
    ) => {
      const { productId } = action.payload;
      const item = state.orderItems.find(
        (item) => item.product.id === productId
      );
      if (item && item.product.stock > item.quantity) item.quantity++;
    },
    decrementQuantity: (
      state,
      action: PayloadAction<{ productId: string }>
    ) => {
      const { productId } = action.payload;
      const item = state.orderItems.find(
        (item) => item.product.id === productId
      );
      if (item && item.quantity > 0) item.quantity--;
    },
    resetOrderList: (state) => {
      state.orderItems = initialState.orderItems;
    },
  },
});
