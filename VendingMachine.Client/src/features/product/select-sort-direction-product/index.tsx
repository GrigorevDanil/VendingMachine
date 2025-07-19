"use client";

import { SortDirection, productListSlice } from "@/entities/product";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import { WhiteSelect } from "@/shared/ui/white-select";
import { SwapVert } from "@mui/icons-material";
import { FormControl, InputLabel, MenuItem } from "@mui/material";
import clsx from "clsx";

export const SelectSortDirectionProduct = ({
  className,
}: {
  className?: string;
}) => {
  const dispatch = useAppDispatch();

  const selectedSortBy = useAppSelector(productListSlice.selectors.sortBy);

  const selectedSortDirection = useAppSelector(
    productListSlice.selectors.sortDirection
  );

  const handleSelectSortBy = (value: SortDirection) =>
    dispatch(productListSlice.actions.setSortDirection(value));

  if (selectedSortBy === "") return;

  return (
    <FormControl className={clsx("w-full", className)}>
      <InputLabel className="text-white" id="select-label-brand">
        <div className="flex items-center gap-2">
          <SwapVert /> Направление сортировки по
        </div>
      </InputLabel>
      <WhiteSelect
        className="text-white flex-1"
        labelId="select-label-brand"
        id="select-brand"
        label="Бренд"
        value={selectedSortDirection}
        onChange={(e) => handleSelectSortBy(e.target.value as SortDirection)}
      >
        <MenuItem value="asc">По возрастанию (от А до Я)</MenuItem>
        <MenuItem value="desc">По убыванию (от Я до А)</MenuItem>
      </WhiteSelect>
    </FormControl>
  );
};
