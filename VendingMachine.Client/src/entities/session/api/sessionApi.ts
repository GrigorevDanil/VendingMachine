import { baseApi } from "@/shared/api";
import { handleError } from "@/shared/lib/handleError";
import { EnvelopeSchema } from "@/shared/model/envelope";
import z from "zod";
import { BusyState } from "../model/busy-state";

const sessionApi = baseApi.injectEndpoints({
  endpoints: (create) => ({
    getBusy: create.query<BusyState, string>({
      query: (idSession) => "api/session?IdSession=" + idSession,
      transformResponse: (response: unknown): BusyState => {
        const result = EnvelopeSchema(z.string()).parse(response);
        return result.result as BusyState;
      },
      transformErrorResponse: (response) => handleError(response),
    }),
    occupySession: create.mutation<BusyState, string>({
      query: (idSession) => ({
        url: "api/session/occupy?IdSession=" + idSession,
        method: "POST",
      }),
      transformErrorResponse: (response) => handleError(response),
    }),
    releaseSession: create.mutation({
      query: () => ({
        url: "api/session/release",
        method: "POST",
      }),
      transformErrorResponse: (response) => handleError(response),
    }),
  }),
  overrideExisting: true,
});

export const {
  useGetBusyQuery,
  useOccupySessionMutation,
  useReleaseSessionMutation,
} = sessionApi;
