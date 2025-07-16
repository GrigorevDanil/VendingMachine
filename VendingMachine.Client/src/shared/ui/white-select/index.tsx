"use client";

import { Select } from "@mui/material";
import { styled } from "@mui/material/styles";

export const WhiteSelect = styled(Select)(() => ({
  "& .MuiOutlinedInput-notchedOutline": {
    borderColor: "white",
  },
  "&:hover .MuiOutlinedInput-notchedOutline": {
    borderColor: "white",
  },
  "&.Mui-focused .MuiOutlinedInput-notchedOutline": {
    borderColor: "#441306",
  },
  "& .MuiSelect-icon": {
    color: "white",
  },
  color: "white",
}));
