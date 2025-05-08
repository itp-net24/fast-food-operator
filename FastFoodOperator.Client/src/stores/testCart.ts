import router from '@/router'
import { defineStore } from 'pinia'
import type { CartContainer } from '@/models/types.ts'
import type {
  CreateOrderDto, OrderComboDtos,
  OrderProductDtos,
  ProductMinimalResponseDto,
  State
} from '@/models/interfaces.ts'
import Fetcher from '@/ApiFetcher.ts'
import { ProductType } from '@/enums/enums.ts'

export const useCart = defineStore('cart', {
  state: (): State => ({
    cart: [],
    receipt: null,
  }),

  actions: {
    clearCart() {
      this.cart = [];
    },

    addToCart(product: CartContainer) {
      if (!product) {
        console.log("BAD IN ADD")
        return;
      }

      this.cart.push(product);
      console.log("CART:", this.cart);
    },

    removeFromCart(product: CartContainer) {
      if (!product) {
        console.log("BAD IN REMOVE");
        return;
      }
      this.cart = this.cart.filter(p => p.id !== product.id);
    },

    checkout(comment: string = '') {
      const fetcher = new Fetcher();

      const products: OrderProductDtos[] = this.cart
        .filter(p => p.type === ProductType.product)
        .map(p => {
          const products = mapToThing(p);

          return { productMinimalResponseDto: products?.[0] }
        })
        .filter((x): x is OrderProductDtos => x !== undefined);


      const combos: OrderComboDtos[] = this.cart
        .filter(p => p.type === ProductType.combo)
        .map(c => {
          const products = mapToThing(c);
          const combo = {
            products: products,
            comboId: c.id,
            quantity: c.quantity,
          }

          return {
            comboMinimalResponseDto: combo,
            comboId: c.id,
            quantity: c.quantity,
          }
        })
        .filter((x): x is  OrderComboDtos => x !== undefined);

      const order: CreateOrderDto = {
        customerNote: comment,
        orderComboDtos: combos,
        orderProductDtos: products,
      }

      console.log("IS WORKING:", order);


      fetcher.createOrder(order)
      .then(response => {
        this.clearCart();
        router.push({
          path: 'test-receipt',
          state: { order: response }
        });
      })
      .catch(error => {
        console.error('Error creating order:', error);
      });
    }
  }
});

export const mapToThing = (container: CartContainer): ProductMinimalResponseDto[] => {
  if (!container || !container.products) {
    console.log("BAD IN PRODUCTS");
  }

  const products: ProductMinimalResponseDto[] = container.products.map(product => ({
    productVariantId: product.variant?.id ?? 0,
    productId: product.id,
    ingredientsId: product.ingredients?.map(i => i.id) ?? [],
    quantity: container.quantity,
  }));

  return products ?? [];
}
