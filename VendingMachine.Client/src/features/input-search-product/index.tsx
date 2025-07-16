import { productListSlice } from "@/entities/product";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import { WhiteTextField } from "@/shared/ui/white-text-field";
import { Search } from "@mui/icons-material";
import clsx from "clsx";

export const InputSearchProduct = ({ className }: { className?: string }) => {
  const dispatch = useAppDispatch();

  const typedTitle = useAppSelector(productListSlice.selectors.typedTitle);

  const handleTextChange = (value: string) =>
    dispatch(productListSlice.actions.setTypedTitle(value));

  return (
    <WhiteTextField
      className={clsx(className)}
      id="input-search-product"
      label={
        <div className="flex items-center gap-2">
          <Search /> Найти напиток по названию
        </div>
      }
      value={typedTitle}
      onChange={(e) => handleTextChange(e.target.value)}
    />
  );
};
