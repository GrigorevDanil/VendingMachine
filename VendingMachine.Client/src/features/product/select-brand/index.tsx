"use client";

import { useGetBrandsQuery } from "@/entities/brand/api";
import { productListSlice } from "@/entities/product";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import { WhiteSelect } from "@/shared/ui/white-select";
import { Clear, Sell } from "@mui/icons-material";
import { FormControl, IconButton, InputLabel, MenuItem } from "@mui/material";
import clsx from "clsx";

export const SelectBrand = ({ className }: { className?: string }) => {
  const dispatch = useAppDispatch();

  const { data: brandResponse, isLoading } = useGetBrandsQuery();

  const selectedBrandId = useAppSelector(
    productListSlice.selectors.selectedBrandId
  );

  const handleSelectBrandId = (brandId: string) => {
    dispatch(productListSlice.actions.setSelectedBrandId(brandId));
    dispatch(productListSlice.actions.setMinPrice(0));
  };

  return (
    <FormControl className={clsx("w-full", className)}>
      <InputLabel className="text-white" id="select-label-brand">
        <div className="flex items-center gap-2">
          <Sell /> Бренд
        </div>
      </InputLabel>
      <div className="flex gap-2 items-center">
        <WhiteSelect
          className="text-white flex-1"
          labelId="select-label-brand"
          id="select-brand"
          label="Бренд"
          value={selectedBrandId}
          onChange={(e) => handleSelectBrandId(e.target.value as string)}
        >
          <MenuItem value="all" divider>
            Все бренды
          </MenuItem>
          {!isLoading &&
            brandResponse?.result?.map((brand) => (
              <MenuItem key={brand.id} value={brand.id}>
                {brand.title}
              </MenuItem>
            ))}
        </WhiteSelect>
        {selectedBrandId && selectedBrandId !== "all" && (
          <IconButton
            className=" text-white bg-red-500 rounded"
            aria-label="clear"
            onClick={() => handleSelectBrandId("all")}
          >
            <Clear />
          </IconButton>
        )}
      </div>
    </FormControl>
  );
};
