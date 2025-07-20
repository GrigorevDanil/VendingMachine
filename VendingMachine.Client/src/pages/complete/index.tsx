"use client";

import { Header } from "@/widgets/header";
import { OrderResult } from "@/widgets/order/order-result";

export const CompletePage = () => {
  return (
    <div className="flex flex-col gap-2 h-full">
      <Header />
      <OrderResult />
    </div>
  );
};
