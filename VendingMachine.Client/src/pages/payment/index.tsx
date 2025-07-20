"use client";

import { PaymentActions } from "@/features/order/payment-actions";
import { CoinList } from "@/widgets/coin/coin-list";
import { CoinOrderSummary } from "@/widgets/coin/coin-order-summary";
import { Header } from "@/widgets/header";

export const PaymentPage = () => {
  return (
    <div className="flex flex-col gap-2 h-full">
      <Header>
        <p> - Оплата заказа</p>
      </Header>
      <CoinList footer={<CoinOrderSummary />} />
      <PaymentActions />
    </div>
  );
};
