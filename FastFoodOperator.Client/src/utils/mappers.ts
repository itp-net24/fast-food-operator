import type {Combo, ComboGroup, ComboProduct, Ingredient, Product, ProductVariant} from "@/models/types.ts";

/* eslint-disable @typescript-eslint/no-explicit-any */
export function mapToProduct(data: any): Product {
  const variants: ProductVariant[] = safeMap(data.variants, mapToProductVariant);

  return {
    id: data.id,
    name: data.name,
    description: data.description ?? null,
    basePrice: data.basePrice,
    imageUrl: data.pictureUrl ?? null,
    variants: variants,
    defaultVariant: variants.find(v => v.id === data.defaultProductVariantId) ?? null,
    ingredients: data.ingredients,
  };
}

export function mapToCombo(data: any): Combo {
  const comboProducts: ComboProduct[] = safeMap(data.comboProducts, mapToComboProduct);

  return {
    id: data.id,
    name: data.name,
    description: data.description ?? null,
    basePrice: data.basePrice,
    imageUrl: data.imageUrl ?? null,

    mainComboProductId: data.mainComboProductId ?? null,
    mainComboProduct: comboProducts.find(cp => cp.id === data.mainComboProductId) ?? null,

    comboProducts: comboProducts,
    comboGroups: safeMap(data.comboGroups, mapToComboGroup),
  };
}

export function mapToComboGroup(data: any): ComboGroup {
  return {
    id: data.id,
    name: data.name,

    defaultComboProductId: data.defaultComboProductId,
    comboProducts: safeMap(data.comboProducts, mapToComboProduct),
  }
}

export function mapToComboProduct(data: any): ComboProduct {
  const product = mapToProduct(data.product);

  return {
    id: data.id,
    product: product,

    defaultProductVariantId: data.defaultProductVariantId,
    defaultProductVariant: product.variants.find(v => v.id === data.defaultProductVariantId) ?? null,
  }
}

export function mapToProductVariant(data: any): ProductVariant {
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

function safeMap<T, R>(array: T[] | null | undefined, mapFn: (item: T) => R): R[] {
  return Array.isArray(array) ? array.map(mapFn) : [];
}
