"use client";

import { Panel } from "@/shared/ui/panel";
import { GoHomeButton } from "@/widgets/routing/go-home-button";
import { Header } from "@/widgets/header";
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
      <Panel className="flex flex-col gap-4 items-center justify-center h-full">
        <p className="text-4xl text-white m-auto">
          Извините, в данный момент автомат занят
        </p>
        <GoHomeButton />
      </Panel>
    </div>
  );
};
