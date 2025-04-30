import type { Product, Ingredient, Combo } from "../models/types.ts";
import {mapToCombo, mapToIngredient, mapToProduct} from "@/utils/mappers.ts";

/* eslint-disable @typescript-eslint/no-explicit-any */
export const GetProductAsync = async (id: number): Promise<Product> => {
    const response = await fetch(`https://localhost:8080/api/product/${id}`);
    const data = await response.json();

    return mapToProduct(data);
}

export const GetIngredientsAsync = async (): Promise<Ingredient[]> => {
    const response = await fetch('https://localhost:8080/api/ingredient');
    const data = await response.json();

    return data.map((i: any) => mapToIngredient(i));
}

export const GetComboAsync = async (id: number): Promise<Combo> => {
    const response = await fetch(`https://localhost:8080/api/combo/${id}`);
    const data = await response.json();

    return mapToCombo(data);
}
