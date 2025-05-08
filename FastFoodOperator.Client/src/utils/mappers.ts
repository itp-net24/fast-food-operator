import type {
  BaseProduct,
  CartContainer,
  CartItem,
  Combo,
  ComboGroup,
  ComboProduct,
  Ingredient,
  Product, Tag,
  Variant
} from '@/models/types.ts'
import { defaultVariantOfProduct, getTaxRate, isProductCombo } from '@/utils/helpers.ts'

/* eslint-disable @typescript-eslint/no-explicit-any */
export const mapToBaseProduct = (data: any): BaseProduct => {
  return {
    id: data.id,
    name: data.name,
    description: data.description ?? null,
    basePrice: data.basePrice,
    imageUrl: data.imageUrl ?? null,
    tags: safeMap(data.tags, mapToTag),
  }
}

export function mapToProduct(data: any): Product {
  const variants: Variant[] = safeMap(data.variants, mapToProductVariant)

  return {
    ...mapToBaseProduct(data),
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
    ...mapToBaseProduct(data),

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

export const  mapToTag = (data: any): Tag => {
  return {
    id: data.id,
    name: data.name,
    tax: data.taxRate,
  }
}

export const mapProductToCart = (p: Product): CartItem => {
  const variant = p.defaultVariant ?? p.variants[0]

  return {
    __uid: -1,
    id: p.id,
    name: p.name,
    basePrice: p.basePrice,
    tax: getTaxRate(p.tags),
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
    basePrice: cp.product.basePrice,
    tax: getTaxRate(cp.product.tags),
    variant: variant ? { ...variant, priceModifier: 0 } : null,
    ingredients: includeIngredients
      ? cp.product.ingredients.map((i) => ({ ...i, priceModifier: 0 }))
      : null,
  }
}

export const mapToCartContainer = (base: BaseProduct, products: CartItem[]): CartContainer => {
  return {
    id: base.id,
    type: isProductCombo(base) ? 'combo' : 'product',
    imageUrl: base.imageUrl,
    name: base.name,
    tags: base.tags,
    price: base.basePrice,
    quantity: 1,
    products: products ?? [],
  }
}

export const mapToOrder = (data:any): Order => {
  return {
    id: data.orderId,
    orderNumber: data.orderNumber,
    customerNote: data.customerNote,
    orderStatus: data.orderStatus,
    createdAt: data.createdAt,
    startedAt: data.startedAt,
    completedAt: data.completedAt,
    orderProducts: data.orderProducts || [],
    orderCombos: data.orderCombos || []
  }
}

function safeMap<T, R>(array: T[] | null | undefined, mapFn: (item: T) => R): R[] {
  return Array.isArray(array) ? array.map(mapFn) : []
}
