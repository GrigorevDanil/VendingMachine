import z from "zod";

export const BrandDtoSchema = z.array(
  z.object({
    id: z.uuid(),
    title: z.string(),
  })
);
