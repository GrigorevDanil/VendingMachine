import { Header } from "@/widgets/header";
import { NavBar } from "@/widgets/navBar";

export const AdminLayout = ({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) => {
  return (
    <div className="flex flex-col gap-2">
      <Header>
        <NavBar />
      </Header>
      {children}
    </div>
  );
};
