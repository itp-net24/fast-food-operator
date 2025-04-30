import type {
  ComboProduct,
  Ingredient,
  Product,
  ProductToCart,
  ProductVariant
} from '@/models/types.ts'

export const mapProductToCart = (p: Product): ProductToCart => {
  return {
    id: p.id,
    name: p.name,
    variant: p.defaultVariant,
    ingredients: p.ingredients.map((i: Ingredient) => ({ id: i.id, name: i.name })),
  }
}

export const mapComboProductToCart = (cp: ComboProduct, variant: ProductVariant | null, mainProduct: Product | null): ProductToCart => {
  const isMainProduct = mainProduct != null && mainProduct.id === cp.id;

  return {
    id: cp.product.id,
    name: cp.product.name,
    variant: variant ? { id: variant.id, name: variant.name } : null,
    ingredients: isMainProduct ? cp.product.ingredients.map((i: Ingredient) => ({
      id: i.id,
      name: i.name,
    })) : null,
  };
};
