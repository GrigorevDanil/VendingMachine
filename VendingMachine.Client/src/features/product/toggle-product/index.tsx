"use client";

import { Button } from "@mui/material";
import clsx from "clsx";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import { Product, ProductStatus } from "@/entities/product/types";
import { orderListSlice } from "@/entities/order";

export const ToggleProduct = ({ product }: { product: Product }) => {
  const dispatch = useAppDispatch();
  const isInOrder = useAppSelector((state) =>
    orderListSlice.selectors.isProductInOrder(state, product.id)
  );

  const getProductStatus = (): ProductStatus => {
    if (product.stock === 0) return "sold_out";
    return isInOrder ? "selected" : "available";
  };

  const productStatus = getProductStatus();

  const getButtonConfig = () => {
    switch (productStatus) {
      case "sold_out":
        return {
          text: "Закончился",
          className: "bg-gray-500 hover:bg-gray-600 cursor-not-allowed",
          disabled: true,
        };
      case "selected":
        return {
          text: "Выбран",
          className: "bg-green-500 hover:bg-green-600",
          disabled: false,
        };
      case "available":
        return {
          text: "Выбрать",
          className: "bg-orange-700 hover:bg-orange-800",
          disabled: false,
        };
      default:
        return {
          text: "Выбрать",
          className: "bg-orange-700 hover:bg-orange-800",
          disabled: false,
        };
    }
  };

  const buttonConfig = getButtonConfig();

  const handleToggleSelection = () => {
    if (productStatus === "available") {
      dispatch(
        orderListSlice.actions.addProductInOrder({
          product,
        })
      );
    } else if (productStatus === "selected") {
      dispatch(orderListSlice.actions.removeProductFromOrder(product.id));
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
