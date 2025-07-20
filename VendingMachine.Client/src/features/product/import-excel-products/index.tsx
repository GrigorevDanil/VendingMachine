import { FileUpload } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useRef } from "react";
import { useImportProductFromExcelMutation } from "@/entities/product/api/productApi";
import { enqueueSnackbar } from "notistack";

export const ImportExcelProducts = () => {
  const [importProductsFromExcel] = useImportProductFromExcelMutation({});

  const fileInputRef = useRef<HTMLInputElement>(null);

  const handleImportClick = () => {
    fileInputRef.current?.click();
  };

  const handleFileChange = async (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const files = event.target.files;
    if (files && files.length > 0) {
      const selectedFile = files[0];

      const fileName = selectedFile.name.toLowerCase();
      if (!fileName.endsWith(".xlsx") && !fileName.endsWith(".xls")) {
        return;
      }

      await importProductsFromExcel(selectedFile).unwrap();

      enqueueSnackbar("Успешный импорт", { variant: "success" });
    }
  };

  return (
    <div className="ml-auto ">
      <input
        type="file"
        ref={fileInputRef}
        onChange={handleFileChange}
        accept=".xlsx,.xls"
        style={{ display: "none" }}
      />
      <Button
        className="w-full text-white bg-gray-500 hover:bg-gray-600 sm:ml-auto sm:w-[200px]"
        size="large"
        startIcon={<FileUpload />}
        onClick={handleImportClick}
      >
        Импорт
      </Button>
    </div>
  );
};
