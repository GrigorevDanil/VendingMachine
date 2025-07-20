export const importProductsFromExcelQuery = (fileExcel: File) => {
  const formData = new FormData();
  formData.append("file", fileExcel);

  return {
    url: "api/product/import",
    method: "POST",
    body: formData,
  };
};
