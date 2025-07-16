import z from "zod";
import { EnvelopeSchema } from "../model/envelope";
import { enqueueSnackbar } from "notistack";

export const ErrorResponseSchema = z.object({
  status: z.number(),
  data: EnvelopeSchema(z.null()).nullable(),
});

export const handleError = async (response: unknown) => {
  const envelopeSchema = EnvelopeSchema(z.null());
  const envelopResponseResult = await envelopeSchema.safeParseAsync(response);

  if (envelopResponseResult.success) {
    const { data: envelopResponse } = envelopResponseResult;

    if (envelopResponse.errors) {
      envelopResponse.errors.forEach((error) => {
        enqueueSnackbar(error.message, { variant: "error" });
      });
    }
    return;
  }

  const errorResponse = await ErrorResponseSchema.safeParseAsync(response);

  if (errorResponse.success) {
    const { data: errorData } = errorResponse;

    if (errorData?.data?.errors) {
      errorData.data.errors.forEach((error) => {
        enqueueSnackbar(error.message, { variant: "error" });
      });
    }
    return;
  }

  enqueueSnackbar("Произошла ошибка с сервером", { variant: "error" });
};
