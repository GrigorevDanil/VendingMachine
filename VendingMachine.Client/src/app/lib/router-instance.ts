import { AppRouterInstance } from "next/dist/shared/lib/app-router-context.shared-runtime";

let routerInstance: AppRouterInstance | null = null;

export const setRouter = (router: AppRouterInstance) => {
  routerInstance = router;
};

export const getRouter = () => {
  if (!routerInstance) {
    throw new Error("Router instance not set!");
  }
  return routerInstance;
};
