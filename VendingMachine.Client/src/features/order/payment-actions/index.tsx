"use client";

import { Panel } from "@/shared/ui/panel";
import { Payment } from "@mui/icons-material";
import { Button } from "@mui/material";
import { GoBackButton } from "../../../widgets/routing/go-back-button";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import clsx from "clsx";
import { orderListSlice } from "@/entities/order";
import { coinListSlice } from "@/entities/coin";
import {
  useCreateOrderWithItemsMutation,
  usePaymentMutation,
} from "@/entities/order/api/orderApi";
import { remainsListSlice } from "@/entities/coin/model/slices/remains-list.slice";
import { useRouter } from "next/navigation";
import { useState } from "react";

export const PaymentActions = () => {
  const router = useRouter();

  const dispatch = useAppDispatch();

  const addedCoins = useAppSelector(coinListSlice.selectors.addedCoins);
  const orderItems = useAppSelector(orderListSlice.selectors.orderItems);

  const sumCoins = useAppSelector(coinListSlice.selectors.sumCoins);
  const sumOrder = useAppSelector(orderListSlice.selectors.sumOrder);

  const [orderId, setOrderId] = useState("");

  const [createOrderWithItems] = useCreateOrderWithItemsMutation();

  const [payment] = usePaymentMutation();

  const isDepositEnough = sumOrder <= sumCoins;

  const handlePayment = async () => {
    let currentOrderId = orderId;

    if (!currentOrderId) {
      const createOrderWithItemsResult = await createOrderWithItems(
        orderItems
      ).unwrap();

      if (createOrderWithItemsResult.result?.orderId) {
        currentOrderId = createOrderWithItemsResult.result.orderId;
        setOrderId(currentOrderId);
      }
    }

    if (currentOrderId) {
      const paymentResult = await payment({
        orderId: currentOrderId,
        coins: addedCoins,
      }).unwrap();

      if (paymentResult.result) {
        dispatch(
          remainsListSlice.actions.setRemains({
            remains: paymentResult.result.remains,
            coins: paymentResult.result.coins,
          })
        );
        dispatch(coinListSlice.actions.resetCoins());
        dispatch(orderListSlice.actions.resetOrderList());
        router.push("/complete");
      }
    }
  };

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
        onClick={handlePayment}
        disabled={!isDepositEnough}
      >
        Оплатить
      </Button>
    </Panel>
  );
};
