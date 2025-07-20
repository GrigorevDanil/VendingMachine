"use client";

import { Panel } from "@/shared/ui/panel";
import { Payment } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useRouter } from "next/navigation";
import { GoBackButton } from "../../../widgets/routing/go-back-button";
import { orderListSlice } from "@/entities/order";
import { useAppSelector } from "@/shared/model/redux";
import clsx from "clsx";

export const OrderActions = () => {
  const router = useRouter();
  const orderItemsCount = useAppSelector(
    orderListSlice.selectors.orderItemsCount
  );

  return (
    <Panel className="flex justify-between">
      <GoBackButton />
      <Button
        variant="outlined"
        size="large"
        className={clsx(
          "text-white",
          orderItemsCount === 0
            ? "bg-gray-500 "
            : "bg-green-500 hover:bg-green-600 "
        )}
        startIcon={<Payment />}
        onClick={() => router.push("/payment")}
        disabled={orderItemsCount === 0}
      >
        Оплата
      </Button>
    </Panel>
  );
};
