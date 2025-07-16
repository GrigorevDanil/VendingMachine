"use client";

import { StoreProvider } from "./store-provider";
import { SnackbarProvider } from "notistack";

export const Providers = ({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) => {
  return (
    <StoreProvider>
      <SnackbarProvider>{children}</SnackbarProvider>
    </StoreProvider>
  );
};
