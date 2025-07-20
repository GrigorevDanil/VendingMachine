import { productListSlice } from "@/entities/product";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import { CustomSlider } from "@/shared/ui/custom-slider";
import clsx from "clsx";

export const PriceSlider = ({ className }: { className?: string }) => {
  const dispatch = useAppDispatch();

  const minPrice = useAppSelector(productListSlice.selectors.minPrice);
  const maxPrice = useAppSelector(productListSlice.selectors.maxPrice);

  return (
    <div className={clsx("w-full p-4", className)}>
      <div className="w-full flex items-center">
        <p className="text-white">0 руб.</p>
        <p className="ml-auto text-white">{maxPrice} руб.</p>
      </div>
      <CustomSlider
        valueLabelDisplay="on"
        valueLabelFormat={(value) => `${value} руб.`}
        value={minPrice}
        max={maxPrice}
        onChangeCommitted={(_, price) =>
          dispatch(productListSlice.actions.setMinPrice(price as number))
        }
      />
    </div>
  );
};
