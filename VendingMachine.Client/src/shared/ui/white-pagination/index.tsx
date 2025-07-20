import { Pagination, styled } from "@mui/material";

export const WhitePagination = styled(Pagination)({
  "& .MuiPaginationItem-root": {
    color: "white",
    borderColor: "rgba(255, 255, 255, 0.5)",
  },
  "& .MuiPaginationItem-page.Mui-selected": {
    backgroundColor: "rgba(255, 255, 255, 0.2)",
    color: "white",
    "&:hover": {
      backgroundColor: "black",
    },
  },
  "& .MuiPaginationItem-page:hover": {
    backgroundColor: "rgba(255, 255, 255, 0.1)",
  },
  "& .MuiSvgIcon-root": {
    fill: "white",
  },
});
