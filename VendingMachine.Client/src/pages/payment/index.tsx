"use client";

import { CoinList } from "@/entities/coin/ui/coin-list";
import { PaymentActions } from "@/features/payment-actions";
import { Header } from "@/widgets/header";

export const PaymentPage = () => {
  return (
    <div className="flex flex-col gap-2 h-full">
      <Header>
        <p> - Оплата заказа</p>
      </Header>
      <CoinList />
      <PaymentActions />
    </div>
  );
};
