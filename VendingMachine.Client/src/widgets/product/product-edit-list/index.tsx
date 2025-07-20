"use client";

import { CircularProgress } from "@mui/material";
import { Panel } from "@/shared/ui/panel";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import {
  productListSlice,
  useGetProductsWithPaginationQuery,
} from "@/entities/product";
import { ProductEditCard } from "@/entities/product/ui/product-edit-card";

export const ProductEditList = () => {
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

  const renderList = () => {
    if (isLoading)
      return (
        <div className="flex">
          <CircularProgress className="text-white m-auto" />
        </div>
      );

    if (productResponse?.result) {
      const { items } = productResponse?.result;

      if (items.length === 0)
        return (
          <div className="flex">
            <p className="text-4xl text-white m-auto">Напитки не найдены</p>
          </div>
        );
      else
        return items.map((product) => (
          <ProductEditCard key={product.id} product={product} />
        ));
    }
  };

  return (
    <Panel className="h-full">
      <div className="grid grid-cols-12 gap-4 mb-4 font-bold text-white">
        <div className="col-span-7">Товар</div>
        <div className="col-span-5 text-right">Количество</div>
      </div>

      <div className="flex flex-col gap-4">{renderList()}</div>
    </Panel>
  );
};
