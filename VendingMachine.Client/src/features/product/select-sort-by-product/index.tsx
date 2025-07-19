"use client";

import { SortByProduct, productListSlice } from "@/entities/product";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import { WhiteSelect } from "@/shared/ui/white-select";
import { Category, Clear } from "@mui/icons-material";
import { FormControl, IconButton, InputLabel, MenuItem } from "@mui/material";
import clsx from "clsx";

export const SelectSortByProduct = ({ className }: { className?: string }) => {
  const dispatch = useAppDispatch();

  const selectedSortBy = useAppSelector(productListSlice.selectors.sortBy);

  const handleSelectSortBy = (value: SortByProduct) =>
    dispatch(productListSlice.actions.setSortBy(value));

  return (
    <FormControl className={clsx("w-full", className)}>
      <InputLabel className="text-white" id="select-label-brand">
        <div className="flex items-center gap-2">
          <Category /> Сортировать по
        </div>
      </InputLabel>
      <div className="flex gap-2 items-center">
        <WhiteSelect
          className="text-white flex-1"
          labelId="select-label-brand"
          id="select-brand"
          label="Сортировать по"
          value={selectedSortBy}
          onChange={(e) => handleSelectSortBy(e.target.value as SortByProduct)}
        >
          <MenuItem value="title">Названию</MenuItem>
          <MenuItem value="price">Цене</MenuItem>
        </WhiteSelect>
        {selectedSortBy && (
          <IconButton
            className=" text-white bg-red-500 rounded"
            aria-label="clear"
            onClick={() => handleSelectSortBy("")}
          >
            <Clear />
          </IconButton>
        )}
      </div>
    </FormControl>
  );
};
