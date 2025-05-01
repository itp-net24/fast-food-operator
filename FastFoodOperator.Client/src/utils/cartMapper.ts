import type {
  ComboProduct,
  Product,
  CartItem,
  Variant,
} from '@/models/types.ts'

export const mapProductToCart = (p: Product): CartItem => {
  return {
    __uid: -1,
    id: p.id,
    name: p.name,
    variant: p.defaultVariant,
    ingredients: p.ingredients,
  }
}

export const mapComboProductToCart = (cp: ComboProduct, variant: Variant | null, mainProduct: Product | null): CartItem => {
  const isMainProduct = mainProduct != null && mainProduct.id === cp.id;

  return {
    __uid: cp.__uid,
    id: cp.product.id,
    name: cp.product.name,
    variant: variant,
    ingredients: isMainProduct ? cp.product.ingredients : null,
  };
};
