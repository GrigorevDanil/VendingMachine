"use client";

import Image from "next/image";
import backgroundImage from "../../assets/background.png";

export const BaseLayout = ({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) => {
  return (
    <div className="relative min-h-screen">
      <div className="fixed inset-0 -z-50 overflow-hidden">
        <Image
          src={backgroundImage}
          alt="background"
          fill
          className="blur-xs object-cover"
          priority
        />
      </div>
      <main className="m-auto relative z-10 p-4 max-w-[1100px] h-screen">
        {children}
      </main>
    </div>
  );
};
