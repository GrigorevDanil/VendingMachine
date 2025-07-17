"use client";

import { ArrowBackRounded } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useRouter } from "next/navigation";

export const GoBackButton = () => {
  const router = useRouter();

  return (
    <Button
      variant="outlined"
      className="border-orange-800 bg-orange-700 text-white hover:bg-orange-800"
      startIcon={<ArrowBackRounded />}
      onClick={() => router.back()}
    >
      Вернуться
    </Button>
  );
};
