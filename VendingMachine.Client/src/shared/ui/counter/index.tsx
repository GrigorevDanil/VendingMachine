import { IconButton, TextField } from "@mui/material";

type Props = {
  currentValue: number;
  maxValue: number;
  minValue?: number;
  onIncrement: () => void;
  onDecrement: () => void;
  onTextChange: (value: string) => void;
};

export const Counter = ({
  currentValue,
  maxValue,
  minValue = 1,
  onIncrement,
  onDecrement,
  onTextChange,
}: Props) => {
  return (
    <div className="flex items-center justify-center gap-2">
      <IconButton
        onClick={onDecrement}
        disabled={currentValue <= minValue}
        size="small"
        sx={{
          width: 32,
          height: 32,
          backgroundColor: "#9F2D00",
          border: "1px solid #d0d0d0",
          borderRadius: "4px",
          color: "#fff",
          "&:hover": {
            backgroundColor: "#441306",
          },
        }}
      >
        -
      </IconButton>

      <TextField
        value={currentValue}
        onChange={(e) => onTextChange(e.target.value)}
        size="small"
        sx={{
          width: "60px",
          "& .MuiOutlinedInput-root": {
            height: "32px",
          },
          "& input": {
            textAlign: "center",
            padding: "8px 4px",
          },
        }}
      />

      <IconButton
        onClick={onIncrement}
        disabled={currentValue >= maxValue}
        size="small"
        sx={{
          width: 32,
          height: 32,
          backgroundColor: "#9F2D00",
          border: "1px solid #d0d0d0",
          borderRadius: "4px",
          color: "#fff",
          "&:hover": {
            backgroundColor: "#441306",
          },
        }}
      >
        +
      </IconButton>
    </div>
  );
};
