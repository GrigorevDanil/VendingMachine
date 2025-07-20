import { Panel } from "@/shared/ui/panel";
import { LocalDrink } from "@mui/icons-material";

export const Header = ({
  children,
}: Readonly<{
  children?: React.ReactNode;
}>) => {
  return (
    <header>
      <Panel className="text-white flex gap-2 items-center text-lg sm:text-2xl flex-wrap sm:flex-nowrap">
        <LocalDrink className="text-2xl sm:text-4xl" />
        <p>Газированные напитки</p>
        {children}
      </Panel>
    </header>
  );
};
