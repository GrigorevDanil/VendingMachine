import { productListSlice } from "@/entities/product";
import { useAppDispatch, useAppSelector } from "@/shared/model/redux";
import { Panel } from "@/shared/ui/panel";
import { WhitePagination } from "@/shared/ui/white-pagination";

export const ProductPagination = () => {
  const dispatch = useAppDispatch();

  const currentPage = useAppSelector(productListSlice.selectors.currentPage);

  const maxPage = useAppSelector(productListSlice.selectors.maxPage);

  return (
    <Panel className="h-full flex justify-center items-center py-4">
      <WhitePagination
        page={currentPage}
        count={maxPage}
        shape="rounded"
        color="primary"
        onChange={(_, page) =>
          dispatch(productListSlice.actions.setCurrentPage(page))
        }
      />
    </Panel>
  );
};
