import { Product } from './models/product'
import { Combo } from './models/combo'
import { Order } from './models/order'
import type { CreateOrderDto } from './models/interfaces';
import { OrderNumber } from './models/orderNumbers';
import { Category } from './models/category';

export default class Fetcher {
  private baseURL: string = "https://localhost:8080/api/";

  async getProducts(limit: number, offset: number): Promise<Product[] | null> {
    try {
      const response = await fetch(`${this.baseURL}product?limit=${limit}&offset=${offset}`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.json();

      return data.map((p: any) => new Product(
        p.id,
        p.name,
        p.description,
        p.basePrice,
        p.imageUrl,
        p.category
      ));

    } catch (error) {
      console.error("Failed to fetch products:", error);
      return null;
    }
  }

  async getProduct(id: number): Promise<Product | null> {
    try {
      const response = await fetch(`${this.baseURL}/product/${id}`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.json();

      return new Product(
        data.id,
        data.name,
        data.description,
        data.category,
        data.basePrice,
        data.pictureUrl
      );
    } catch (error) {
      console.error("Failed to fetch product:", error);
      return null;
    }
  }


  async getCombos(): Promise<Combo[] | null> {
    try {
      const response = await fetch(`${this.baseURL}combo`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.json();

      return data.map((o: any) => new Combo(
        o.id,
        o.name,
        o.basePrice
      ));

    } catch (error) {
      console.error("Failed to fetch products:", error);
      return null;
    }
  }

  async getCategories(): Promise<Category[] | null> {
    try {
      const response = await fetch(`${this.baseURL}category`)
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.json();

      return data.map((c: any) => new Category(
        c.id,
        c.name
      ));
    } catch (error) {
      console.error("failed to fetch categories:", error);
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
        o.id,
        o.orderNumber,
        o.customerNote,
        o.orderStatus,
        o.createdAt,
        o.startedAt,
        o.completedAt
       ));
     } catch (error) {
       console.error("Failed to fetch orders:", error);
       return null;
     }
   }

   //Används inte
   async getOrderNumbers(): Promise<OrderNumber | null> {
      try {
        const response = await fetch(`${this.baseURL}order/displayOrderNumbers`);
        if (!response.ok) {
          throw new Error(`Http error! status: ${response.status}`);
        }

        const data = await response.json();

        return new OrderNumber(
          data.orderNumber,
          data.orderStatus
        )
      } catch (error) {
        console.error("Failed to fetch ordernumbers:", error);
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
        console.log(order);
        return result;
      } catch (error) {
        console.error("Failed to create order:", error);
        throw error;
      }
   }

   async getProductsByCategoryId(id: number, limit: number, offset: number): Promise<Product[] | null> {
    try {
      const response = await fetch(`${this.baseURL}Product/GetProductsByCategory?id=${id}&limit=${limit}&offset=${offset}`);
      if (!response.ok) {
        throw new Error(`Http error! status: ${response.status}`);
      }

      const data = await response.json();
      return data.map((p: any) => new Product(
        p.id,
        p.name,
        p.description,
        p.basePrice,
        p.imageUrl,
        p.category
      ));
    } catch(error) {
      console.error("Failed to fetch products by categoryId");
      return null;
    }
  }
}
