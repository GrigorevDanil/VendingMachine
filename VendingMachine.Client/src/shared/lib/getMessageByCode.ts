export const getMessageByCode = (
  serverMessage: string,
  code: string
): string => {
  switch (code) {
    case "NOT_ENOUGH_AVAILABLE_COINS":
      return "Извините, в данный момент мы не можем продать вам товар по причине того, что автомат не может выдать вам нужную сдачу";

    default:
      return serverMessage;
  }
};
