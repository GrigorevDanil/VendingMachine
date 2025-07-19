"use client";

import { Header } from "@/widgets/header";
import { IsBusy } from "@/widgets/isBusy";
import { useRouter } from "next/navigation";
import { useEffect } from "react";

export const IsBusyPage = () => {
  const router = useRouter();

  useEffect(() => {
    const intervalId = setInterval(() => {
      router.push("/");
    }, 60000);

    return () => clearInterval(intervalId);
  }, [router]);

  return (
    <div className="flex flex-col gap-2 h-full">
      <Header />
      <IsBusy />
    </div>
  );
};
