"use client";

import { ImportExcelProducts } from "@/features/product/import-excel-products";
import { ProductFilter } from "@/features/product/product-filter";
import { ProductPagination } from "@/features/product/product-pagination";
import { Header } from "@/widgets/header";
import { ProductList } from "@/widgets/product/product-list";

export const HomePage = () => {
  return (
    <div className="flex flex-col gap-2 h-full">
      <Header>
        <ImportExcelProducts />
      </Header>
      <ProductFilter />
      <ProductList />
      <ProductPagination />
    </div>
  );
};
