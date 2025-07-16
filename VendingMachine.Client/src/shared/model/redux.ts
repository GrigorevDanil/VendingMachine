import { makeStore } from "@/app/lib/make-store";
import { createSelector } from "@reduxjs/toolkit/react";
import { useDispatch, useSelector, useStore } from "react-redux";

export type AppStore = ReturnType<typeof makeStore>;
export type AppState = ReturnType<AppStore["getState"]>;
export type AppDispatch = AppStore["dispatch"];

export const useAppSelector = useSelector.withTypes<AppState>();
export const useAppDispatch = useDispatch.withTypes<AppDispatch>();
export const useAppStore = useStore.withTypes<AppStore>();

export const createAppSelector = createSelector.withTypes<AppState>();
