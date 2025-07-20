import { Panel } from "@/shared/ui/panel";
import { SelectBrand } from "../../../features/product/select-brand";
import { PriceSlider } from "../../../features/product/price-slider";
import { Button } from "@mui/material";
import clsx from "clsx";
import { SelectSortByProduct } from "../../../features/product/select-sort-by-product";
import { SelectSortDirectionProduct } from "../../../features/product/select-sort-direction-product";
import { InputSearchProduct } from "../../../features/product/input-search-product";
import { useAppSelector } from "@/shared/model/redux";
import { useRouter } from "next/navigation";
import { orderListSlice } from "@/entities/order";

export const ProductFilter = ({
  goToOrder = true,
}: {
  goToOrder?: boolean;
}) => {
  const router = useRouter();

  const orderItemsCount = useAppSelector(
    orderListSlice.selectors.orderItemsCount
  );

  const isDisabled = orderItemsCount === 0;

  return (
    <Panel className="flex flex-col gap-2">
      <div className="flex gap-2 items-center justify-between flex-col sm:flex-row sm:gap-10">
        <SelectBrand className="flex-1" />
        <PriceSlider className="flex-1" />
        {goToOrder && (
          <Button
            className={clsx(
              "text-white w-full sm:w-[200px]",
              isDisabled ? "bg-gray-500" : "bg-green-500 hover:bg-green-600"
            )}
            size="large"
            disabled={isDisabled}
            onClick={() => router.push("/order")}
          >
            Выбранно ({orderItemsCount})
          </Button>
        )}
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
