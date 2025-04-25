import { Product } from './models/product'
import { Order } from './models/order'
import type { CreateOrderDto } from './models/interfaces';

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

       const data = await response.json();
       return data.map((o: any) => new Order(
        data.id,
        data.orderNumber,
        data.customerNote,
        data.orderStatus,
        data.createdAt,
        data.startedAt,
        data.completedAt
       ));
     } catch (error) {
       console.error("Failed to fetch orders:", error);
       return null;
     }
   }

   async createOrder(order: CreateOrderDto) : Promise<any> {
      try {
        const response = await fetch((`${this.baseURL}order/add`), {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(order)
      });
        if (!response.ok) {
          throw new Error(`Failed to add order! status: ${response.status}`);
        }
        const result = await response.json();
        return result;
      } catch (error) {
        console.error("Failed to create order:", error);
        throw error;
      }
   }
}
