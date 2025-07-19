"use client";

import { Panel } from "@/shared/ui/panel";
import { Payment } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useRouter } from "next/navigation";
import { GoBackButton } from "../../widgets/go-back-button";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import clsx from "clsx";
import { orderListSlice } from "@/entities/order";
import { coinListSlice } from "@/entities/coin";
import {
  useCreateOrderWithItemsMutation,
  usePaymentMutation,
} from "@/entities/order/api/orderApi";
import { remainsListSlice } from "@/entities/coin/model/remains-list.slice";

export const PaymentActions = () => {
  const dispatch = useAppDispatch();

  const router = useRouter();

  const addedCoins = useAppSelector(coinListSlice.selectors.addedCoins);
  const orderItems = useAppSelector(orderListSlice.selectors.orderItems);
  const remainsCoins = useAppSelector(remainsListSlice.selectors.coins);

  const sumCoins = useAppSelector(coinListSlice.selectors.sumCoins);
  const sumOrder = useAppSelector(orderListSlice.selectors.sumOrder);

  const [createOrderWithItems] = useCreateOrderWithItemsMutation();

  const [payment] = usePaymentMutation();

  const isDepositEnough = sumOrder <= sumCoins;

  const handlePayment = async () => {
    console.log("start", remainsCoins);

    const createOrderWithItemsResult = await createOrderWithItems(
      orderItems
    ).unwrap();

    if (createOrderWithItemsResult?.result?.orderId) {
      const paymentResult = await payment({
        orderId: createOrderWithItemsResult?.result?.orderId,
        coins: addedCoins,
      }).unwrap();

      if (paymentResult.result) {
        dispatch(
          remainsListSlice.actions.setPaymentResult(paymentResult.result)
        );

        console.log("end", remainsCoins);

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
