import type {Combo, ComboGroup, ComboProduct, Ingredient, Product, ProductVariant} from "../../../../todo/src/models/types.ts";

export function mapToProduct(data: any): Product {
    return {
        id: data.id,
        name: data.name,
        description: data.description ?? null,
        basePrice: data.basePrice,
        imageUrl: data.pictureUrl ?? null,
        variants: safeMap(data.variants, mapToProductVariant),
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
