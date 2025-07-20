import { CoinImage } from "@/widgets/coin/coin-image";
import { Coin } from "../model/coin";

export const CountCoinCard = ({ coin }: { coin: Coin }) => {
  return (
    <div className="flex gap-2 items-center bg-white rounded p-2 text-xl">
      <CoinImage denomination={coin.denomination} />
      <p>{coin.stock} шт.</p>
    </div>
  );
};
