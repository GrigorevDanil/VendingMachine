import { Panel } from "@/shared/ui/panel";

export const IsBusy = () => {
  return (
    <Panel className="flex items-center justify-center h-full">
      <p className="text-4xl text-white m-auto">
        Извините, в данный момент автомат занят
      </p>
    </Panel>
  );
};
