import type {
  CartItem,
  Combo,
  ComboGroup,
  ComboProduct,
  Ingredient,
  Product,
  Variant,
} from '@/models/types.ts'
import { defaultVariantOfProduct } from '@/utils/helpers.ts'

/* eslint-disable @typescript-eslint/no-explicit-any */
export function mapToProduct(data: any): Product {
  const variants: Variant[] = safeMap(data.variants, mapToProductVariant)

  return {
    id: data.id,
    name: data.name,
    description: data.description ?? null,
    basePrice: data.basePrice,
    imageUrl: data.pictureUrl ?? null,
    variants: variants,
    defaultVariant: variants.find((v) => v.id === data.defaultProductVariantId) ?? null,
    ingredients: data.ingredients,
  }
}

export function mapToCombo(data: any): Combo {
  const uidRef = { value: 0 }

  const comboProducts: ComboProduct[] = safeMap(data.comboProducts, (d) =>
    mapToComboProduct(d, uidRef),
  )
  const comboGroups: ComboGroup[] = safeMap(data.comboGroups, (d) => mapToComboGroup(d, uidRef))

  return {
    id: data.id,
    name: data.name,
    description: data.description ?? null,
    basePrice: data.basePrice,
    imageUrl: data.imageUrl ?? null,

    mainComboProductId: data.mainComboProductId ?? null,
    mainComboProduct: comboProducts.find((cp) => cp.id === data.mainComboProductId) ?? null,

    comboProducts: comboProducts,
    comboGroups: comboGroups,
  }
}

export function mapToComboGroup(data: any, uidRef: { value: number }): ComboGroup {
  const comboProducts = safeMap(data.comboProducts, (d) => mapToComboProduct(d, uidRef))

  return {
    id: data.id,
    name: data.name,

    defaultComboProductId: data.defaultComboProductId,
    defaultComboProduct: comboProducts.find((cp) => cp.id === data.defaultComboProductId) ?? null,
    comboProducts: comboProducts,
  }
}

export function mapToComboProduct(data: any, uidRef: { value: number }): ComboProduct {
  const product = mapToProduct(data.product)

  return {
    __uid: uidRef.value++,
    id: data.id,
    product: product,

    defaultProductVariantId: data.defaultProductVariantId,
    defaultProductVariant:
      product.variants.find((v) => v.id === data.defaultProductVariantId) ?? null,
  }
}

export function mapToProductVariant(data: any): Variant {
  return {
    id: data.id,
    name: data.name,
    priceModifier: data.priceModifier,
  }
}

export function mapToIngredient(data: any): Ingredient {
  return {
    id: data.id,
    name: data.name,
    priceModifier: data.priceModifier,
  }
}

export const mapProductToCart = (p: Product): CartItem => {
  const variant = p.defaultVariant ?? p.variants[0]

  return {
    __uid: -1,
    id: p.id,
    name: p.name,
    variant: variant ? { ...variant, priceModifier: 0 } : null,
    ingredients: p.ingredients.map((i) => ({ ...i, priceModifier: 0 })),
  }
}

export const mapComboProductToCart = (cp: ComboProduct, includeIngredients: boolean): CartItem => {
  const variant = defaultVariantOfProduct(cp)

  return {
    __uid: cp.__uid,
    id: cp.product.id,
    name: cp.product.name,
    variant: variant ? { ...variant, priceModifier: 0 } : null,
    ingredients: includeIngredients
      ? cp.product.ingredients.map((i) => ({ ...i, priceModifier: 0 }))
      : null,
  }
}

function safeMap<T, R>(array: T[] | null | undefined, mapFn: (item: T) => R): R[] {
  return Array.isArray(array) ? array.map(mapFn) : []
}
