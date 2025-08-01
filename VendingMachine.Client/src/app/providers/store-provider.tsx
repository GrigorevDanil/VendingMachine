"use client";

import { useRef } from "react";
import { Provider } from "react-redux";
import { makeStore } from "../lib/make-store";
import { AppStore } from "@/shared/model/redux";

export const StoreProvider = ({ children }: { children: React.ReactNode }) => {
  const storeRef = useRef<AppStore | null>(null);
  if (!storeRef.current) {
    storeRef.current = makeStore();
  }

  return <Provider store={storeRef.current}>{children}</Provider>;
};
