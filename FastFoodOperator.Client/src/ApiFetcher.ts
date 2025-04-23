import {Product} from './models/product'

export default class Fetcher {
  private baseURL: string = "https://localhost:8080/api/";

  async getProducts(): Promise<Product[] | null> {
    try {
      const response = await fetch(`${this.baseURL}product`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.json();

      return data.map((p: any) => new Product(
        p.id,
        p.name,
        p.description,
        p.basePrice,
        p.pictureUrl
      ));

    } catch (error) {
      console.error("Failed to fetch products:", error);
      return null;
    }
  }

  async getProduct(id: number): Promise<Product | null> {
    try {
      const response = await fetch(`${this.baseURL}product/${id}`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.json();

      return new Product(
        data.id,
        data.name,
        data.description,
        data.basePrice,
        data.pictureUrl
      );
    } catch (error) {
      console.error("Failed to fetch product:", error);
      return null;
    }
  }

  async getOrders(): Promise<Order[] | null> {
    try {
      const response = await fetch(`${this.baseURL}order/GetOrders`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return await response.json() as Order[];
    } catch (error) {
      console.error("Failed to fetch orders:", error);
      return null;
    }
  }

  async getOrdernumbers(): Promise<number[] | null> {
    try {
      const response = await fetch(`${this.baseURL}order/displayOrderNumbers`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return await response.json() as number[];
    } catch (error) {
      console.error("Failed to fetch order numbers:", error);
      return null;
    }
  }
}
