import { coinListSlice } from "@/entities/coin";
import { useReplenishBalanceMutation } from "@/entities/coin/api/coinApi";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import { Panel } from "@/shared/ui/panel";
import { Payment } from "@mui/icons-material";
import { Button } from "@mui/material";
import clsx from "clsx";
import { enqueueSnackbar } from "notistack";

export const BalanceActions = () => {
  const dispatch = useAppDispatch();

  const [replenishBalance] = useReplenishBalanceMutation();
  const sumCoins = useAppSelector(coinListSlice.selectors.sumCoins);
  const addedCoins = useAppSelector(coinListSlice.selectors.addedCoins);

  const handleReplenishBalance = async () => {
    await replenishBalance(addedCoins);
    enqueueSnackbar("Успешно пополнено на " + sumCoins + " руб.", {
      variant: "success",
    });
    dispatch(coinListSlice.actions.resetCoins());
  };

  return (
    <Panel className="flex justify-end">
      <Button
        variant="outlined"
        size="large"
        className={clsx(
          "text-white",
          sumCoins === 0 ? "bg-gray-500 " : "bg-green-500 hover:bg-green-600 "
        )}
        startIcon={<Payment />}
        disabled={sumCoins === 0}
        onClick={handleReplenishBalance}
      >
        Пополнить
      </Button>
    </Panel>
  );
};
