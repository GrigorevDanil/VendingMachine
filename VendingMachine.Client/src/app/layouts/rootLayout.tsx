import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "../styles";
import { BaseLayout } from "@/shared/ui/base-layout";
import { AppRouterCacheProvider } from "@mui/material-nextjs/v15-appRouter";
import { Footer } from "@/widgets/footer";
import { Providers } from "../providers";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Автомат с газированными напитками",
  description: "Пет проект созданный Григорьевым Д.В.",
};

export const RootLayout = ({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) => {
  return (
    <html lang="ru">
      <body
        className={`m-auto ${geistSans.variable} ${geistMono.variable} antialiased`}
      >
        <AppRouterCacheProvider options={{ enableCssLayer: true }}>
          <BaseLayout>
            <div className="flex flex-col gap-2">
              <Providers> {children}</Providers>
              <Footer />
            </div>
          </BaseLayout>
        </AppRouterCacheProvider>
      </body>
    </html>
  );
};
