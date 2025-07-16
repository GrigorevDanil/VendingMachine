import { Panel } from "@/shared/ui/panel";
import { SelectBrand } from "../select-brand";
import { PriceSlider } from "../price-slider";
import { Button } from "@mui/material";
import clsx from "clsx";
import { SelectSortByProduct } from "../select-sort-by-product";
import { SelectSortDirectionProduct } from "../select-sort-direction-product";
import { InputSearchProduct } from "../input-search-product";
import { useAppSelector } from "@/shared/model/redux";
import { productListSlice } from "@/entities/product";

export const ProductFilter = () => {
  const selectedProductsCount = useAppSelector(
    productListSlice.selectors.selectProductsCount
  );

  const isDisabled = selectedProductsCount === 0;

  return (
    <Panel className="flex flex-col gap-2">
      <div className="flex gap-2 items-center justify-between flex-col sm:flex-row sm:gap-10">
        <SelectBrand className="flex-1" />
        <PriceSlider className="flex-1" />
        <Button
          className={clsx(
            "text-white w-full sm:w-[200px]",
            isDisabled ? "bg-gray-500" : "bg-green-600"
          )}
          size="large"
          disabled={isDisabled}
        >
          Выбрано ({selectedProductsCount})
        </Button>
      </div>
      <div className="flex gap-2 items-center justify-between flex-col sm:flex-row sm:gap-10">
        <SelectSortByProduct className="flex-1" />
        <SelectSortDirectionProduct className="flex-1" />
      </div>
      <div className="flex gap-2 items-center justify-between flex-col sm:flex-row sm:gap-10">
        <InputSearchProduct className="flex-1" />
      </div>
    </Panel>
  );
};
