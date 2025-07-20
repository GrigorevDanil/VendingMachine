"use client";

import { Button } from "@mui/material";
import { useRouter } from "next/navigation";
import { GoHomeButton } from "../routing/go-home-button";

export const NavBar = () => {
  const router = useRouter();

  const navigation = [
    { route: "/admin/balance", title: "Баланс" },
    { route: "/admin/product", title: "Товары" },
  ];

  return (
    <div className="flex flex-1 justify-between items-center gap-2 bg-white rounded p-2">
      <div className="flex gap-2">
        {navigation.map((item, index) => (
          <Button
            key={index}
            className="text-white bg-orange-700 hover:bg-orange-800"
            onClick={() => router.push(item.route)}
          >
            {item.title}
          </Button>
        ))}
      </div>
      <GoHomeButton />
    </div>
  );
};
