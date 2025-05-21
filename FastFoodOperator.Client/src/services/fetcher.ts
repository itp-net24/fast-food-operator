import type { Product, Ingredient, Combo, BaseProduct, Tag, Order } from '../models/types.ts'
import {
  mapToBaseProduct,
  mapToCombo,
  mapToIngredient,
  mapToProduct,
  mapToTag
} from '@/utils/mappers.ts'
import { API_BASE_PATH } from '../../config.ts'

/* eslint-disable @typescript-eslint/no-explicit-any */
export const fetchJson = async (url: string, options?: RequestInit): Promise<any> => {
  try {
    const response = await fetch(API_BASE_PATH + url, options);
    if (!response.ok)
      throw new Error(`HTTP error! status: ${response.status}`);

    if (response.status === 204) return;
    return await response.json();
  } catch (error) {
    console.error("Error fetching from API!", error);
  }
};


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

export const getTagsAsync = async (): Promise<Tag[]> => {
  const data = await fetchJson('api/tag?limit=100');

  return data.map(t => mapToTag(t)) ?? [];
}
export const StartOrder = async (order: any): Promise<any> => {
  const data = await fetchJson('api/order/startorder', {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(order)
  })
}

export const CompleteOrder = async (order:any): Promise<any> => {
  const data = await fetchJson('api/order/completeorder', {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(order)
  })

  return data;
}

export const DeleteOrder = async (id: number): Promise<void> => {
  await fetchJson(`api/order/${id}`, {
    method: 'DELETE'
  });
};

export const GetOrders = async(): Promise<Order[] | null> => {
  const response = await fetchJson('api/order/getorders');
  if (!response || response.length === 0) {
    return [];
  }
  return response;
}
