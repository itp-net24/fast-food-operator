import type {Combo, ComboGroup, ComboProduct, Ingredient, Product, Variant} from "@/models/types.ts";

/* eslint-disable @typescript-eslint/no-explicit-any */
export function mapToProduct(data: any): Product {
  const variants: Variant[] = safeMap(data.variants, mapToProductVariant);

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
  const uidRef = { value: 0 };

  const comboProducts: ComboProduct[] = safeMap(data.comboProducts, d => mapToComboProduct(d, uidRef));
  const comboGroups: ComboGroup[] = safeMap(data.comboGroups, d => mapToComboGroup(d, uidRef));

  return {
    id: data.id,
    name: data.name,
    description: data.description ?? null,
    basePrice: data.basePrice,
    imageUrl: data.imageUrl ?? null,

    mainComboProductId: data.mainComboProductId ?? null,
    mainComboProduct: comboProducts.find(cp => cp.id === data.mainComboProductId) ?? null,

    comboProducts: comboProducts,
    comboGroups: comboGroups,
  };
}

export function mapToComboGroup(data: any, uidRef: { value: number }): ComboGroup {
  const comboProducts = safeMap(data.comboProducts, d => mapToComboProduct(d, uidRef));

  return {
    id: data.id,
    name: data.name,

    defaultComboProductId: data.defaultComboProductId,
    defaultComboProduct: comboProducts.find(cp => cp.id === data.defaultComboProductId) ?? null,
    comboProducts: comboProducts,
  }
}

export function mapToComboProduct(data: any, uidRef: { value: number }): ComboProduct {
  const product = mapToProduct(data.product);

  return {
    __uid: uidRef.value++,
    id: data.id,
    product: product,

    defaultProductVariantId: data.defaultProductVariantId,
    defaultProductVariant: product.variants.find(v => v.id === data.defaultProductVariantId) ?? null,
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

function safeMap<T, R>(array: T[] | null | undefined, mapFn: (item: T) => R): R[] {
  return Array.isArray(array) ? array.map(mapFn) : [];
}
