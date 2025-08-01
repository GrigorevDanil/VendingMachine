import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BASE_URL } from "./constants";

export const baseApi = createApi({
  baseQuery: fetchBaseQuery({ baseUrl: BASE_URL, credentials: "include" }),
  endpoints: () => ({}),
  tagTypes: ["Product", "Brand", "Coin"],
});
