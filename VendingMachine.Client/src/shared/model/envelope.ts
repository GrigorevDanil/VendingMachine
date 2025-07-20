import z from "zod";
import { Error, ErrorSchema } from "./error";

export const EnvelopeSchema = <T extends z.ZodTypeAny>(resultSchema: T) =>
  z.object({
    result: resultSchema.nullable(),
    errors: z.array(ErrorSchema).nullable(),
    timeGenerated: z.iso.datetime({ offset: true }),
  });

export type Envelope<T> = {
  result: T | null;
  errors: Error[] | null;
  timeGenerated: string;
};
