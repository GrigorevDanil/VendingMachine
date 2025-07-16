"use client";

import { ProductList } from "@/entities/product/ui/product-list";
import { ProductFilter } from "@/features/product-filter";
import { ProductPagination } from "@/features/product-pagination";
import { Header } from "@/widgets/header";
import { FileUpload } from "@mui/icons-material";
import { Button } from "@mui/material";

export const HomePage = () => {
  return (
    <div className="flex flex-col gap-2 h-full">
      <Header>
        <Button
          className="w-full text-white bg-gray-600 sm:ml-auto sm:w-[200px]"
          size="large"
          startIcon={<FileUpload />}
        >
          Импорт
        </Button>
      </Header>
      <ProductFilter />
      <ProductList />
      <ProductPagination />
    </div>
  );
};
