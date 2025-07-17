export enum Denomination {
  One,
  Two,
  Five,
  Ten,
}

export type Coin = {
  denomination: Denomination;
  stock: number;
};

export const getDenomination = (coin: Coin) => {
  switch (coin.denomination) {
    case Denomination.One:
      return 1;
    case Denomination.Two:
      return 2;
    case Denomination.Five:
      return 5;
    case Denomination.Ten:
      return 10;
    default:
      return 1;
  }
};
