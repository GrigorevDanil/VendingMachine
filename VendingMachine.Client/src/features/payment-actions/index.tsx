"use client";

import { Panel } from "@/shared/ui/panel";
import { Payment } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useRouter } from "next/navigation";
import { GoBackButton } from "../go-back-button";
import { useAppSelector } from "@/shared/model/redux";
import clsx from "clsx";
import { coinListSlice } from "@/entities/coin/slices/coin-list.slise";
import { orderListSlice } from "@/entities/order";
import { getDenomination } from "@/entities/coin/model/coin";

export const PaymentActions = () => {
  const router = useRouter();
  const coins = useAppSelector(coinListSlice.selectors.coins);
  const orderItems = useAppSelector(orderListSlice.selectors.orderItems);

  const sum = orderItems.reduce(
    (sum, item) => sum + item.product.price * item.quantity,
    0
  );

  const deposit = coins.reduce(
    (sum, item) => sum + getDenomination(item) * item.stock,
    0
  );

  const isDepositEnough = deposit > sum;

  return (
    <Panel className="flex justify-between">
      <GoBackButton />
      <Button
        variant="outlined"
        size="large"
        className={clsx(
          "text-white",
          !isDepositEnough ? "bg-gray-500 " : "bg-green-500 hover:bg-green-600 "
        )}
        startIcon={<Payment />}
        onClick={() => router.push("/payment")}
        disabled={!isDepositEnough}
      >
        Оплатить
      </Button>
    </Panel>
  );
};
