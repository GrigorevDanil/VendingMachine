import { productListSlice } from "@/entities/product";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import { WhiteTextField } from "@/shared/ui/white-text-field";
import { Search } from "@mui/icons-material";
import clsx from "clsx";
import { useEffect, useState } from "react";

export const InputSearchProduct = ({
  className,
  duration = 300,
}: {
  className?: string;
  duration?: number;
}) => {
  const dispatch = useAppDispatch();
  const typedTitle = useAppSelector(productListSlice.selectors.typedTitle);
  const [inputValue, setInputValue] = useState(typedTitle);

  useEffect(() => {
    const timer = setTimeout(() => {
      dispatch(productListSlice.actions.setTypedTitle(inputValue));
    }, duration);

    return () => clearTimeout(timer);
  }, [inputValue, duration, dispatch]);

  const handleTextChange = (value: string) => {
    setInputValue(value);
  };

  return (
    <WhiteTextField
      className={clsx("w-full", className)}
      id="input-search-product"
      label={
        <div className="flex items-center gap-2">
          <Search /> Найти напиток по названию
        </div>
      }
      value={inputValue}
      onChange={(e) => handleTextChange(e.target.value)}
    />
  );
};
