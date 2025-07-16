"use client";

import clsx from "clsx";

export const Panel = ({
  children,
  className,
}: Readonly<{
  children?: React.ReactNode;
  className?: string;
}>) => {
  return (
    <div className={clsx("relative p-4", className)}>
      <div className="absolute inset-0 bg-orange-800 opacity-80 rounded-lg -z-10 " />
      {children}
    </div>
  );
};
