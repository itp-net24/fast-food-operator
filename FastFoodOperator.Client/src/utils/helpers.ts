import type {
  CartContainer, Combo, ComboGroup, ComboProduct, Variant
} from '@/models/types.ts'
import { PRECISION_INTERNAL } from '../../config.ts'

export const calculateCartContainerTotal = (container: CartContainer, precision: number = PRECISION_INTERNAL) => {
  const total = container.products.reduce((acc, p) => {
    const variantCost = p.variant?.priceModifier ?? 0
    const ingredientsTotal = p.ingredients?.reduce((acc, i) => acc + i.priceModifier, 0) ?? 0;

    return acc + variantCost + ingredientsTotal;
  }, container.price)

  return roundToPrecision(total, precision);
}

export const getVariantDiscount = (variant: Variant, defaultVariant: Variant | null = null, precision: number = PRECISION_INTERNAL) => {
  const value = Math.max(variant.priceModifier - (defaultVariant?.priceModifier ?? 0), 0);

  return roundToPrecision(value, precision);
}

export const roundToPrecision = (value: number, precision: number): number => {
  const factor = Math.pow(100, precision);
  return Math.round(value * factor) / factor;
}

export const clamp = (value: number, min: number, max: number): number => {
  return Math.min(max, Math.max(min, value));
}

export const defaultProductOfCombo = (combo: Combo): ComboProduct | null => combo.mainComboProduct ?? combo.comboProducts[0] ?? null;
export const defaultProductOfGroup = (group: ComboGroup): ComboProduct | null => group.defaultComboProduct ?? group.comboProducts[0] ?? null;
export const defaultVariant = (product: ComboProduct): Variant | null => product.defaultProductVariant ?? product.product.variants[0] ?? null;
