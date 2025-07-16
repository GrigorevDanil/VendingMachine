import { BASE_URL } from "@/shared/api/constants";
import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  images: {
    remotePatterns: [new URL(BASE_URL + "/images/**")],
  },
};

export default nextConfig;
