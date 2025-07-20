"use client";

import { TextField } from "@mui/material";
import { styled } from "@mui/material/styles";
import { inputLabelClasses } from "@mui/material/InputLabel";

export const WhiteTextField = styled(TextField)(() => ({
  color: "white",
  [`& .${inputLabelClasses.root}`]: {
    color: "white",
    "&.Mui-focused": {
      color: "white",
    },
  },
  "& .MuiOutlinedInput-root": {
    color: "white",
    "& fieldset": {
      borderColor: "white",
    },
    "&:hover fieldset": {
      borderColor: "white",
    },
    "&.Mui-focused fieldset": {
      borderColor: "#441306",
    },
  },
  "& .MuiSelect-icon": {
    color: "white",
  },
  "& .MuiInputBase-input": {
    color: "white",
    "&::placeholder": {
      color: "rgba(255, 255, 255, 0.7)",
      opacity: 1,
    },
  },
}));
