import type { Product, Ingredient, Combo } from "../models/types.ts";
import { mapToBaseProduct, mapToCombo, mapToIngredient, mapToProduct } from '@/utils/mappers.ts'
import { API_BASE_PATH } from '../../config.ts'

/* eslint-disable @typescript-eslint/no-explicit-any */
export const fetchJson = async (url: string): Promise<any> => {
  try {
    const response = await fetch(API_BASE_PATH + url);
    if (!response.ok)
      throw new Error(`HTTP error! status: ${response.status}`);

    const result = await response.json();
    return result;
  }
  catch (error) {
    console.error("Error fetching from API!", error);
  }
}

export const getProductsAsync = async (limit: number, offset: number): Promise<BaseProduct[]> => {
  const data = await fetchJson(`api/product?limit=${limit}&offset=${offset}`)

  return data.map(p => mapToBaseProduct(p));
}

export const getProductsByTagAsync = async (id: number): Promise<BaseProduct[]> => {
  const data = await fetchJson(`api/Product/GetProductsByCategory?id=${id}`);

  return data.map(p => mapToBaseProduct(p));
}

export const GetProductAsync = async (id: number): Promise<Product> => {
    const data = await fetchJson(`api/product/${id}`);

    return mapToProduct(data);
}

export const GetComboAsync = async (id: number): Promise<Combo> => {
    const data = await fetchJson(`api/combo/${id}`);

    return mapToCombo(data);
}

export const GetIngredientsAsync = async (): Promise<Ingredient[]> => {
  const data = await fetchJson('api/ingredient');

  return data.map(i => mapToIngredient(i));
}
