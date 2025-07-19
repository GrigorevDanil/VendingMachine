import { PayloadAction, createSlice } from "@reduxjs/toolkit/react";
import { BrandId } from "@/entities/brand/types";

export type SortByProduct = "title" | "price" | "";

export type SortDirection = "asc" | "desc";

export type State = {
  currentPage: number;
  maxPage: number;
  selectedBrandId: BrandId;
  minPrice: number;
  maxPrice: number;
  sortBy: SortByProduct;
  sortDirection: SortDirection;
  typedTitle: string;
};

const initialState: State = {
  currentPage: 1,
  maxPage: 1,
  selectedBrandId: "",
  minPrice: 0,
  maxPrice: 0,
  sortBy: "",
  sortDirection: "asc",
  typedTitle: "",
};

export const productListSlice = createSlice({
  name: "product-list",
  initialState,
  selectors: {
    currentPage: (state: State) => state.currentPage,
    maxPage: (state: State) => state.maxPage,
    selectedBrandId: (state: State) => state.selectedBrandId,
    minPrice: (state: State) => state.minPrice,
    maxPrice: (state: State) => state.maxPrice,
    sortBy: (state: State) => state.sortBy,
    sortDirection: (state: State) => state.sortDirection,
    typedTitle: (state: State) => state.typedTitle,
  },
  reducers: {
    setCurrentPage: (state, action: PayloadAction<number>) => {
      state.currentPage = action.payload;
    },
    setMaxPage: (state, action: PayloadAction<number>) => {
      state.maxPage = action.payload;
    },
    setSelectedBrandId: (state, action: PayloadAction<string>) => {
      state.selectedBrandId = action.payload;
    },
    setMinPrice: (state, action: PayloadAction<number>) => {
      state.minPrice = action.payload;
    },
    setMaxPrice: (state, action: PayloadAction<number>) => {
      state.maxPrice = action.payload;
    },
    setSortBy: (state, action: PayloadAction<SortByProduct>) => {
      state.sortBy = action.payload;
    },
    setSortDirection: (state, action: PayloadAction<SortDirection>) => {
      state.sortDirection = action.payload;
    },
    setTypedTitle: (state, action: PayloadAction<string>) => {
      state.typedTitle = action.payload;
    },
  },
});
