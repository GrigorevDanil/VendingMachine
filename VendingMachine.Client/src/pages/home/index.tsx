"use client";

import { ProductList } from "@/entities/product/ui/product-list";
import {
  useGetBusyQuery,
  useOccupySessionMutation,
  useReleaseSessionMutation,
} from "@/entities/session/api/sessionApi";
import { ProductFilter } from "@/features/product-filter";
import { ProductPagination } from "@/features/product-pagination";
import { Header } from "@/widgets/header";
import { FileUpload } from "@mui/icons-material";
import { Button } from "@mui/material";
import { v4 as randomUUID } from "uuid";
import { useRouter } from "next/navigation";
import { useEffect, useState, useRef } from "react";
import { useImportProductFromExcelMutation } from "@/entities/product/api/productApi";
import { enqueueSnackbar } from "notistack";

export const HomePage = () => {
  const router = useRouter();
  const fileInputRef = useRef<HTMLInputElement>(null);

  const [idSession] = useState<string>(randomUUID());

  const [occupySession] = useOccupySessionMutation({});

  const [releaseSession] = useReleaseSessionMutation({});

  const { data: isBusy } = useGetBusyQuery(idSession);

  const [importProductsFromExcel] = useImportProductFromExcelMutation({});

  useEffect(() => {
    if (isBusy === "Close") {
      router.push("/isBusy");
    } else if (isBusy === "Open") {
      occupySession(idSession);
    }

    const handleBeforeUnload = () => {
      releaseSession({});
    };

    window.addEventListener("beforeunload", handleBeforeUnload);

    return () => {
      window.removeEventListener("beforeunload", handleBeforeUnload);
      releaseSession({});
    };
  }, [idSession, isBusy, occupySession, releaseSession, router]);

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

      const formData = new FormData();
      formData.append("file", selectedFile);

      await importProductsFromExcel(formData).unwrap();

      enqueueSnackbar("Успешный импорт", { variant: "success" });
    }
  };

  return (
    <div className="flex flex-col gap-2 h-full">
      <Header>
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
      </Header>
      <ProductFilter />
      <ProductList />
      <ProductPagination />
    </div>
  );
};
