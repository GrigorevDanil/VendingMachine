import { Slider, styled } from "@mui/material";

export const CustomSlider = styled(Slider)({
  color: "#441306",
  height: 8,
  "& .MuiSlider-track": {
    border: "none",
  },
  "& .MuiSlider-thumb": {
    height: 24,
    width: 24,
    backgroundColor: "#fff",
    border: "2px solid currentColor",
    "&:focus, &:hover, &.Mui-active, &.Mui-focusVisible": {
      boxShadow: "inherit",
    },
    "&::before": {
      display: "none",
    },
  },
  "& .MuiSlider-valueLabel": {
    lineHeight: 1,
    fontSize: 14,
    fontWeight: "bold",
    background: "unset",
    backgroundColor: "#441306",
    borderRadius: "4px",
    padding: "4px 8px",
    width: "auto",
    minWidth: "32px",
    height: "auto",
    top: "100%",
    transform: "translateY(0) scale(0)",
    "&::before": {
      display: "none",
    },
    "&.MuiSlider-valueLabelOpen": {
      transform: "translateY(20%) scale(1)",
    },
    "& > *": {
      transform: "none",
      whiteSpace: "nowrap",
    },
  },
});
