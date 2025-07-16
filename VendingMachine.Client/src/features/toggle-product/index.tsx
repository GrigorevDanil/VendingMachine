"use client";

import { Button } from "@mui/material";
import clsx from "clsx";
import { productListSlice } from "@/entities/product";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import { Product, ProductStatus } from "@/entities/product/types";

export const ToggleProduct = ({ product }: { product: Product }) => {
  const dispatch = useAppDispatch();
  const isSelected = useAppSelector((state) =>
    productListSlice.selectors.isProductSelected(state, product.id)
  );

  const getProductStatus = (): ProductStatus => {
    if (product.stock === 0) return "sold_out";
    return isSelected ? "selected" : "available";
  };

  const productStatus = getProductStatus();

  const getButtonConfig = () => {
    switch (productStatus) {
      case "sold_out":
        return {
          text: "Закончился",
          className: "bg-gray-500 cursor-not-allowed",
          disabled: true,
        };
      case "selected":
        return {
          text: "Выбрано",
          className: "bg-green-500",
          disabled: false,
        };
      case "available":
        return {
          text: "Выбрать",
          className: "bg-orange-800",
          disabled: false,
        };
      default:
        return {
          text: "Выбрать",
          className: "bg-orange-800",
          disabled: false,
        };
    }
  };

  const buttonConfig = getButtonConfig();

  const handleToggleSelection = () => {
    if (productStatus === "available" || productStatus === "selected") {
      dispatch(productListSlice.actions.toggleProductSelection(product.id));
    }
  };

  return (
    <Button
      className={clsx("w-full text-white", buttonConfig.className)}
      onClick={handleToggleSelection}
      disabled={buttonConfig.disabled}
    >
      {buttonConfig.text}
    </Button>
  );
};
