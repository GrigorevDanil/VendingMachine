import z from "zod";

export enum ErrorType {
  Validation,
  NotFound,
  Failure,
  Conflict,
  Null,
}

export const ErrorSchema = z.object({
  code: z.string(),
  message: z.string(),
  type: z.enum(ErrorType),
  invalidField: z.string().optional().nullable(),
});

export type Error = z.infer<typeof ErrorSchema>;
