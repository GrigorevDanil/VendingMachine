"use client";

import { ArrowBackRounded } from "@mui/icons-material";
import { Button } from "@mui/material";
import clsx from "clsx";
import { useRouter } from "next/navigation";

export const GoHomeButton = ({ className }: { className?: string }) => {
  const router = useRouter();

  return (
    <Button
      variant="outlined"
      className={clsx(
        "border-orange-800 bg-orange-700 text-white hover:bg-orange-800",
        className
      )}
      startIcon={<ArrowBackRounded />}
      onClick={() => router.push("/")}
    >
      Каталог напитков
    </Button>
  );
};
