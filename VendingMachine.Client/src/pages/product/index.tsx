"use client";

import { ProductFilter } from "@/features/product/product-filter";
import { ProductPagination } from "@/features/product/product-pagination";
import { ProductEditList } from "@/widgets/product/product-edit-list";

export const ProductPage = () => {
  return (
    <div className="flex flex-col gap-2">
      <ProductFilter goToOrder={false} />
      <ProductEditList />
      <ProductPagination />
    </div>
  );
};
