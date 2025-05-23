import type {
  BaseProduct,
  CartContainer,
  CartItem,
  Combo,
  ComboGroup,
  ComboProduct,
  PriceSummary,
  Tag,
  TaxSummary,
  Variant
} from '@/models/types.ts'
import { PRECISION_INTERNAL } from '../../config.ts'

export const getProductTotalPrice = (container: CartContainer, precision: number = PRECISION_INTERNAL) => {
  const total = container.products.reduce((acc, p) => {
    const variantCost = p.variant?.priceModifier ?? 0
    const ingredientsTotal = p.ingredients?.reduce((acc, i) => acc + i.priceModifier, 0) ?? 0;

    return acc + variantCost + ingredientsTotal;
  }, container.price)

  return roundToPrecision(total, precision);
}

// Initial prices include tax!
export const getProductPriceSummary = (container: CartContainer, precision: number = PRECISION_INTERNAL): TaxSummary => {
  const totalProductCost: number = container.products.reduce((acc: number, p: CartItem) => {
    const variantCost: number = p.variant?.priceModifier ?? 0;
    const ingredientsCost: number = p.ingredients?.reduce((acc, i) => acc + i.priceModifier, 0) ?? 0;

    return acc + p.basePrice + variantCost + ingredientsCost;
  }, 0);

  const productWeightMap: Map<CartItem, number> = container.products.reduce((map: Map<CartItem, number>, p: CartItem) => {
    const productCost = p.basePrice + (p.variant?.priceModifier ?? 0) + (p.ingredients?.reduce((acc, i) => acc + i.priceModifier, 0) ?? 0);

    map.set(p, productCost / totalProductCost);
    return map;
  }, new Map<CartItem, number>());

  const priceSummary = container.products.reduce((acc: PriceSummary, p) => {
    const total = productWeightMap.get(p)! * getProductTotalPrice(container);
    acc.total += total;

    const net = total / p.tax;
    acc.net += net;
    acc.gross += total - net;

    return acc;
  }, { total: 0, net: 0, gross: 0 });

  (['total', 'net', 'gross'] as const).forEach(key => {
    priceSummary[key] = roundToPrecision(priceSummary[key], precision);
  });

  return { taxRate: getTaxRate(container.tags), priceSummary: priceSummary };
}

export const getTaxRate = (tags: Tag[]) => {
  return Math.max( ...tags.map(t => t.tax));
}

export const getVariantDiscount = (variant: Variant, defaultVariant: Variant | null = null, precision: number = PRECISION_INTERNAL) => {
  const value = Math.max(variant.priceModifier - (defaultVariant?.priceModifier ?? 0), 0);

  return roundToPrecision(value, precision);
}

export const isProductCombo = (product: BaseProduct): boolean => {
  if (!product || !product.tags) return false;

  return product.tags.some(tag => tag.name.toLowerCase() === 'combo');
}

export const roundToPrecision = (value: number, precision: number): number => {
  const factor = Math.pow(10, precision);
  return Math.round(value * factor) / factor;
}

export const clamp = (value: number, min: number, max: number): number => {
  return Math.min(max, Math.max(min, value));
}

export const defaultProductOfCombo = (combo: Combo): ComboProduct | null => combo.mainComboProduct ?? combo.comboProducts[0] ?? null;
export const defaultProductOfGroup = (group: ComboGroup): ComboProduct | null => group.defaultComboProduct ?? group.comboProducts[0] ?? null;
export const defaultVariantOfProduct = (product: ComboProduct): Variant | null => product.defaultProductVariant ?? product.product?.variants?.[0] ?? null;
