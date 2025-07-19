"use client";

import { remainsListSlice } from "@/entities/coin/model/remains-list.slice";
import { useAppSelector } from "@/shared/model/redux";
import { Panel } from "@/shared/ui/panel";
import { CoinImage } from "@/widgets/coin-image";
import { Header } from "@/widgets/header";
import { ArrowBackRounded } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useRouter } from "next/navigation";

export const CompletePage = () => {
  const router = useRouter();

  const remains = useAppSelector(remainsListSlice.selectors.remains);
  const coins = useAppSelector(remainsListSlice.selectors.coins);
  const isRemains = useAppSelector(remainsListSlice.selectors.isRemains);

  return (
    <div className="flex flex-col gap-2 h-full">
      <Header />
      <Panel className="flex flex-col gap-2 items-center justify-center">
        <div className="text-4xl text-white">Спасибо за покупку!</div>
        {isRemains && (
          <>
            <div className="flex text-4xl items-center gap-2 text-white">
              <p>Пожалуйста, возьмите вашу сдачу: </p>

              <p className="text-green-500">{` ${remains} руб.`}</p>
            </div>
            <div className="text-4xl text-white mt-10">Ваши монеты:</div>
            {coins.map((coin) => (
              <div
                key={coin.denomination}
                className="flex gap-2 items-center bg-white rounded p-2 text-xl"
              >
                <CoinImage denomination={coin.denomination} />
                <p>{coin.quantity} шт.</p>
              </div>
            ))}
          </>
        )}
        <Button
          variant="outlined"
          className="border-orange-800 bg-orange-700 text-white hover:bg-orange-800"
          startIcon={<ArrowBackRounded />}
          size="large"
          onClick={() => router.push("/")}
        >
          Каталог напитков
        </Button>
      </Panel>
    </div>
  );
};
