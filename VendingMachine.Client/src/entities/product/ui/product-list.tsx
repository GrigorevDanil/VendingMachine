"use client";

import { CircularProgress } from "@mui/material";
import { useGetProductsWithPaginationQuery } from "../api/productApi";
import { ProductCard } from "./product-card";
import { Panel } from "@/shared/ui/panel";
import { useEffect } from "react";
import { productListSlice } from "../api";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";

export const ProductList = () => {
  const dispatch = useAppDispatch();

  const currentPage = useAppSelector(productListSlice.selectors.currentPage);

  const selectedBrandId = useAppSelector(
    productListSlice.selectors.selectedBrandId
  );

  const minPrice = useAppSelector(productListSlice.selectors.minPrice);

  const sortBy = useAppSelector(productListSlice.selectors.sortBy);

  const sortDirection = useAppSelector(
    productListSlice.selectors.sortDirection
  );

  const typedTitle = useAppSelector(productListSlice.selectors.typedTitle);

  const { data: productResponse, isLoading } =
    useGetProductsWithPaginationQuery({
      Page: currentPage,
      PageSize: 8,
      BrandId: selectedBrandId,
      MinPrice: minPrice,
      SortBy: sortBy,
      SortDirection: sortDirection,
      Title: typedTitle,
    });

  useEffect(() => {
    if (
      productResponse?.result?.items &&
      productResponse.result.items.length > 0 &&
      productResponse.result.options
    ) {
      dispatch(
        productListSlice.actions.setMaxPage(productResponse.result.pageCount)
      );

      dispatch(
        productListSlice.actions.setMaxPrice(
          productResponse.result.options?.maxPrice
        )
      );
    }
  }, [productResponse, isLoading, dispatch]);

  return (
    <Panel className="h-full">
      {isLoading ? (
        <div className="flex justify-center items-center">
          <CircularProgress className="text-white" />
        </div>
      ) : (
        <div className="grid grid-cols-2 sm:grid-cols-4 gap-6">
          {productResponse?.result?.items.map((product) => (
            <ProductCard key={product.id} product={product} />
          ))}
        </div>
      )}
    </Panel>
  );
};
