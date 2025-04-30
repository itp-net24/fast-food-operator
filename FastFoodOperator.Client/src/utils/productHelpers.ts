import type { Combo, ComboGroup, ComboProduct, Product, ProductVariant } from '@/models/types.ts'

export const getProductPrice = (product: Product, variant?: ProductVariant): number => {
  const v = variant ? variant : product.defaultVariant ?? product.variants ? product.variants[0] : null;
  return product.basePrice + (v?.priceModifier ?? 0);
}

export const getComboPrice = (combo: Combo, getSelectedProduct: (cg: ComboGroup) => ComboProduct | null, getSelectedVariant: (cp: ComboProduct | null) => ProductVariant | null): number => {
  const comboTotal = combo.comboProducts.reduce((acc, cp) => {
    const variant = getSelectedVariant(cp);
    const selectedModifier = variant?.priceModifier ?? 0;
    const defaultModifier = variant?.priceModifier ?? 0;

    return acc + selectedModifier - defaultModifier;
  }, 0);

  const groupTotal = combo.comboGroups.reduce((acc, cg) => {
    const cp = getSelectedProduct(cg);
    const variant= getSelectedVariant(cp);

    const selectedModifier = variant?.priceModifier ?? 0;
    const defaultModifier = cp?.defaultProductVariant?.priceModifier ?? 0;

    return acc + Math.max(selectedModifier - defaultModifier, 0);
  }, 0);

  const mp = combo.mainComboProduct?.product ?? combo.comboProducts[0].product
  const ingredientsTotal = mp.ingredients.reduce((acc, i) => {
    return acc + i.priceModifier;
  }, 0);

  return Math.round((combo.basePrice + comboTotal + groupTotal + ingredientsTotal) * 100) / 100;
}
