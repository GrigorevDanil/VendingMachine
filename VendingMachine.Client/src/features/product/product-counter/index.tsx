import { Counter } from "@/shared/ui/counter";
import { Product } from "@/entities/product/types";
import { useUpdateProductStockMutation } from "@/entities/product";

export const ProductCounter = ({ product }: { product: Product }) => {
  const [updateProductStock] = useUpdateProductStockMutation();

  const handleQuantityChange = async (value: string) => {
    if (/^\d*$/.test(value)) {
      updateProductStock({ productId: product.id, stock: parseInt(value) });
    }
  };

  return (
    <Counter
      currentValue={product.stock}
      maxValue={999}
      onIncrement={() =>
        updateProductStock({ productId: product.id, stock: product.stock + 1 })
      }
      onDecrement={() =>
        updateProductStock({ productId: product.id, stock: product.stock - 1 })
      }
      onTextChange={(value) => handleQuantityChange(value)}
    />
  );
};
