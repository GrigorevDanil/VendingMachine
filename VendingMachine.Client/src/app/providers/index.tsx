"use client";

import { SnackbarProvider } from "notistack";
import { StoreProvider } from "./store-provider";
import { useRouter } from "next/navigation";
import { useEffect } from "react";
import { setRouter } from "../lib";

export const Providers = ({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) => {
  const router = useRouter();

  useEffect(() => {
    setRouter(router);
  }, [router]);

  return (
    <StoreProvider>
      <SnackbarProvider>{children}</SnackbarProvider>
    </StoreProvider>
  );
};
