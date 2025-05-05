import { defineStore } from "pinia";
import { Product } from '@/models/product'
import type { Cart, State, AddOrderDTO, OrderProductDto, } from '@/models/interfaces'
import fetcher from '@/ApiFetcher'
import type { CartContainer, CartItem } from '@/models/types'


export const useCartStore = defineStore('cart', {
  state: (): State => ({
    cart: { cartProducts: [], cartCombos: [] }
  }),

  actions: {
    loadCartInstance() {
      const cs = localStorage.getItem('cart')
      if (!cs)
        this.cart = { cartProducts: [], cartCombos: [] }
      else
        this.cart = JSON.parse(cs)
    },

    addToCart(pro: CartContainer) {
      console.log('start of addToCart')
      console.log(pro)
      const cs = localStorage.getItem('cart')

      let isAdded = false

      //determine if cart is empty
      console.log(!cs)
      if (!cs) {
        console.log('type:' + pro.type)
        if (pro.type === 'product') {
          this.cart = {
            cartProducts: [
              {
                id: pro.id,
                type: pro.type,
                imageUrl: pro.imageUrl,
                name: pro.name,
                price: pro.price,
                quantity: pro.quantity,
                products: pro.products
              }],
            cartCombos: []
          }
        }

        else if (pro.type === 'combo') {
          this.cart = {
            cartProducts: [],
            cartCombos: [
              {
                id: pro.id,
                type: pro.type,
                imageUrl: pro.imageUrl,
                name: pro.name,
                price: pro.price,
                quantity: pro.quantity,
                products: pro.products
              }]
          }
        }
        else {
          console.error('this type is not supported' + pro.type)
        }

      }

      //logic for when cart is not empty
      else {
        let cartLocalStorage = JSON.parse(cs)
        console.log('cart:' + cartLocalStorage)

        console.log('type:' + pro.type)
        //determine cartcontainer type (combo or product)
        if (pro.type === 'product') {
          // adding product to cart
          cartLocalStorage.cartProducts = cartLocalStorage.cartProducts.map((cpi: CartContainer) => {
            if (cpi.id === pro.id) {
              isAdded = true;
              return {
                id: pro.id,
                type: pro.type,
                imageUrl: pro.imageUrl,
                name: pro.name,
                price: pro.price,
                quantity: cpi.quantity + pro.quantity,
                products: pro.products
              }
            }
            return {
              id: cpi.id,
              type: cpi.type,
              imageUrl: cpi.imageUrl,
              name: cpi.name,
              price: cpi.price,
              quantity: cpi.quantity,
              products: cpi.products
            }
          })

          if (!isAdded) {
            cartLocalStorage.cartProducts.push({
              id: pro.id,
              type: pro.type,
              imageUrl: pro.imageUrl,
              name: pro.name,
              price: pro.price,
              quantity: pro.quantity,
              products: pro.products
            })
          }

          this.cart = cartLocalStorage;

        }

        else if (pro.type === 'combo') {
          // adding existing combo to cart
          cartLocalStorage.cartCombos = cartLocalStorage.cartCombos.map((cci: CartContainer) => {
            if (cci.id === pro.id) {
              isAdded = true;
              return {
                id: pro.id,
                type: pro.type,
                imageUrl: pro.imageUrl,
                name: pro.name,
                price: pro.price,
                quantity: cci.quantity + pro.quantity,
                products: pro.products
              }
            }
            return {
              id: cci.id,
              type: cci.type,
              imageUrl: cci.imageUrl,
              name: cci.name,
              price: cci.price,
              quantity: cci.quantity,
              products: cci.products
            }
          })
          //adding new combo that is not already in cart
          if (!isAdded) {
            cartLocalStorage.cartCombos.push({
              id: pro.id,
              type: pro.type,
              imageUrl: pro.imageUrl,
              name: pro.name,
              price: pro.price,
              quantity: pro.quantity,
              products: pro.products
            })
          }
        }

        this.cart = cartLocalStorage;

        console.log(localStorage.getItem('cart'))

        localStorage.setItem('cart', JSON.stringify(this.cart))

      }
    },

    decrementFromCart(pro: Product) {
      console.log('start of decrementFromCart')
      const cs = localStorage.getItem('cart')
    
      let remove = false
    
      console.log(!cs)
      if (!cs) {
        this.cart = {
          cartProducts: [
          ]
        }
      }
      else {
    
        console.log(localStorage.getItem('cart'))
    
        let cartLocalStorage = JSON.parse(cs)
        console.log(cartLocalStorage)
        cartLocalStorage.cartProducts = cartLocalStorage.cartProducts.map((ci: cartProduct) => {
          if (ci.product.id === pro.id) {
            if (ci.qty <= 1) {
              remove = true
            }
            else {
              return { product: pro, qty: ci.qty - 1 }
            }
          }
          return { product: ci.product, qty: ci.qty }
        })
        console.log('before remove check' + remove)
        if (remove) {
          cartLocalStorage.cartProducts = cartLocalStorage.cartProducts.filter((ci: cartProduct) => ci.product.id != pro.id)
        }
    
        this.cart = cartLocalStorage
    
      }
    
      localStorage.setItem('cart', JSON.stringify(this.cart))
    
    
    },

    removeFromCart(pro: Product) {
      (this.cart as Cart).cartProducts = (this.cart as Cart).cartProducts.filter(ci => ci.product.id != pro.id)
      localStorage.setItem('cart', JSON.stringify(this.cart))
    },

    clearCart() {
      (this.cart as Cart) = { cartProducts: [], cartCombos: [] }
      localStorage.setItem('cart', JSON.stringify(this.cart))
    },

    checkOut(comment: string = '') {
      console.log('in the checkout action' + comment)
    
      let order: AddOrderDTO = {
        customerNote: comment,
        orderComboDtos: [
          {
            comboMinimalResponseDto: {
              products: [
                {
                  productVariantId: number,
                  productId: number,
                  ingredientsId: [
                    number
                  ],
                  quantity: number
                }
              ],
              comboId: number,
              quantity: number
            }
          }
        ],
        orderProductDtos: [
          {
            productMinimalResponseDto: {
              productVariantId: number,
              productId: number,
              ingredientsId: [
                number
              ],
              quantity: number
            }
          }
        ]
      }
      console.log(order.orderProductDtos)
    
      console.log(JSON.stringify(order))
    
      //set up array for orderProductDtos
      let orderProductDtosArray = []
      //map all products in cart to orderProductDtos
      order.orderProductDtos = (this.cart as Cart).cartProducts.map((ci: cartProduct) => {
        return {
          productId: ci.product.id,
          quantity: ci.qty
        }
      })
      //order.orderProductDtos.push(orderProductDtosArray)
    
      //verify that orderProductDtos were mapped correctly
      console.log(order.orderProductDtos)
      console.log(JSON.stringify(order))
      //set up array for orderComboDtos
      // let orderComboDtos = []
      // orderComboDtos = (this.cart as Cart).cartOrders.map((co:cartOrder) =>{
      //     return {
      //         comboId: co.order.id,
      //         quantity:co.qty
      //       }
      // })
      // console.log('array of ordercomboDtos '+orderComboDtos)
    
    
    
      this.createOrder(order)
    
      // empty cart for next order
      this.clearCart()
    },

    async createOrder(order: OrderDTO): Promise < any > {
      try {
        const baseURL = 'https://localhost:8080/api/'
            const response = await fetch((`${baseURL}order/add`), {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(order)
        });
        if(!response.ok) {
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
})
