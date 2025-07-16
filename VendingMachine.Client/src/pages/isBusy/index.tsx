"use client";

import { useGetBusyQuery } from "@/entities/session";
import { Header } from "@/widgets/header";
import { IsBusy } from "@/widgets/isBusy";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { v4 as randomUUID } from "uuid";

export const IsBusyPage = () => {
  const router = useRouter();

  const [idSession] = useState<string>(randomUUID());

  const { data: isBusy } = useGetBusyQuery(idSession);

  useEffect(() => {
    if (isBusy === "Open") {
      router.push("/");
    }
  }, [idSession, isBusy, router]);

  return (
    <div className="flex flex-col gap-2 h-full">
      <Header />
      <IsBusy />
    </div>
  );
};
