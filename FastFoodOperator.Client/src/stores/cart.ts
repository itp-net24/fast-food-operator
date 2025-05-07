import { defineStore } from "pinia";
import { Product } from '@/models/product'
import type { Cart, State, AddOrderDTO, CreateOrderDto, ProductMinimalResponseDto, ComboMinimalResponseDto, OrderComboDtos, OrderProductDtos  } from '@/models/interfaces'
import Fetcher from '@/ApiFetcher'
import type { CartContainer, CartItem } from '@/models/types'
import {ProductType} from '@/enums/enums'


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
      console.log(JSON.stringify(pro))
      const cs = localStorage.getItem('cart')

      let isAdded = false

      //determine if cart is empty
      console.log(!cs)
      console.log('if!cs start here')
      if (!cs) {
        console.log('type:' + pro.type)
        if (pro.type === ProductType.product) {
          this.cart = {
            cartProducts: [
              {
                id: pro.id,
                type: pro.type,
                imageUrl: pro.imageUrl,
                name: pro.name,
                tags:pro.tags,
                price: pro.price,
                quantity: pro.quantity,
                products: pro.products
              }],
            cartCombos: []
          }
        }
        else if (pro.type === ProductType.combo) {
          this.cart = {
            cartProducts: [],
            cartCombos: [
              {
                id: pro.id,
                type: pro.type,
                imageUrl: pro.imageUrl,
                name: pro.name,
                tags:pro.tags,
                price: pro.price,
                quantity: pro.quantity,
                products: pro.products
              }]
          }
        }
        else {
          console.error('this type is not supported, type: ' + pro.type)
        }

      }

      //logic for when cart is not empty
      else {
        console.log("else cart not empty starts here")
        console.log(cs)
        let cartLocalStorage = JSON.parse(cs)
        console.log('cart:' + cartLocalStorage)

        console.log('type:' + pro.type)
        //determine cartcontainer type (combo or product)
        if (pro.type === ProductType.product) {
          // adding product to cart
          cartLocalStorage.cartProducts = cartLocalStorage.cartProducts.map((cpi: CartContainer) => {
            if (cpi.id === pro.id) {
              isAdded = true;
              let newProduct:CartContainer = {
                id: pro.id,
                type: pro.type,
                imageUrl: pro.imageUrl,
                name: pro.name,
                tags: pro.tags,
                price: pro.price,
                quantity: cpi.quantity + pro.quantity,
                products: pro.products
              }
              return newProduct
            }

            let existingProduct:CartContainer = {
              id: cpi.id,
              type: cpi.type,
              imageUrl: cpi.imageUrl,
              name: cpi.name,
              tags: cpi.tags,
              price: cpi.price,
              quantity: cpi.quantity,
              products: cpi.products
            } 
            return existingProduct
          })

          if (!isAdded) {
            let addProduct:CartContainer = {
              id: pro.id,
              type: pro.type,
              imageUrl: pro.imageUrl,
              name: pro.name,
              tags: pro.tags,
              price: pro.price,
              quantity: pro.quantity,
              products: pro.products
            }
            cartLocalStorage.cartProducts.push(addProduct)
          }

          this.cart = cartLocalStorage;

        }

        else if (pro.type === ProductType.combo) {
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

       

      }

      localStorage.setItem('cart', JSON.stringify(this.cart))
    },

    // decrementFromCart(pro: Product) {
    //   console.log('start of decrementFromCart')
    //   const cs = localStorage.getItem('cart')
    
    //   let remove = false
    
    //   console.log(!cs)
    //   if (!cs) {
    //     this.cart = {
    //       cartProducts: [
    //       ]
    //     }
    //   }
    //   else {
    
    //     console.log(localStorage.getItem('cart'))
    
    //     let cartLocalStorage = JSON.parse(cs)
    //     console.log(cartLocalStorage)
    //     cartLocalStorage.cartProducts = cartLocalStorage.cartProducts.map((ci: cartProduct) => {
    //       if (ci.product.id === pro.id) {
    //         if (ci.qty <= 1) {
    //           remove = true
    //         }
    //         else {
    //           return { product: pro, qty: ci.qty - 1 }
    //         }
    //       }
    //       return { product: ci.product, qty: ci.qty }
    //     })
    //     console.log('before remove check' + remove)
    //     if (remove) {
    //       cartLocalStorage.cartProducts = cartLocalStorage.cartProducts.filter((ci: cartProduct) => ci.product.id != pro.id)
    //     }
    
    //     this.cart = cartLocalStorage
    
    //   }
    
    //   localStorage.setItem('cart', JSON.stringify(this.cart))
    
    
    // },

    removeFromCart(pro: Product) {
      (this.cart as Cart).cartProducts = (this.cart as Cart).cartProducts.filter(ci => ci.id != pro.id)
      localStorage.setItem('cart', JSON.stringify(this.cart))
    },

    clearCart() {
      (this.cart as Cart) = { cartProducts: [], cartCombos: [] }
      localStorage.setItem('cart', JSON.stringify(this.cart))
    },

    checkOut(comment: string = '') {
      const fetcher = new Fetcher();

      console.log('in the checkout action ' + comment)
      console.log('list of combos in cart: ' + JSON.stringify(this.cart.cartCombos))
      console.log('list of products in cart: ' + JSON.stringify(this.cart.cartProducts))
      console.log('cart at the beginnning of checkout: ' + JSON.stringify(this.cart))
    
      let checkOutCombos:OrderComboDtos[] = []
      let checkOutProducts:OrderProductDtos[] = []

      console.log('after setting default pseudo-empty values for checkOutCombos and checkOutProducts')
      console.log('checkOutCombos values: ' + JSON.stringify(checkOutCombos))
      console.log('checkOutProducts values: ' + JSON.stringify(checkOutProducts))
    
      //map all products in cart to orderProductDtos
      console.log(this.cart.cartProducts.length < 1)
      if(this.cart.cartProducts.length > 0)
      {
        checkOutProducts = (this.cart as Cart).cartProducts.map((ci: CartContainer):OrderProductDtos => {
            return{
              productMinimalResponseDto:{
            productVariantId: ci.products[0].variant?.id ?? 0,
            productId: ci.id,
            IngredientsId: (ci.products.map(pi => pi.ingredients?.map(i => i.id) ?? []).flat()),
            quantity: ci.quantity
            }}
        })
      }
      //map((vi:CartItem) => vi.variant?.id ?? 0) lol
    //ci.products.map(pi => pi.ingredients?.Map(i => i.id).flat())
      //ci.products.map(pi => pi.ingredients?.forEach((i) => {return i.id}))
      //ci.products.map(pi => pi.ingredients?.map(i => i.id) ?? [].flat())

      //verify that orderProductDtos were mapped correctly
      console.log('after mapping checkOutProducts' + checkOutProducts)
      console.log(JSON.stringify(checkOutProducts))

      console.log('length of cartCombos: ' + this.cart.cartCombos.length)
      console.log(this.cart.cartCombos.length > 0)

    if(this.cart.cartCombos.length > 0){
      checkOutCombos = (this.cart as Cart).cartCombos.map((co:CartContainer):OrderComboDtos => {
        return {
          comboMinimalResponseDto:{
            products:co.products.map((ci:CartItem):ProductMinimalResponseDto =>{
              return{
                productVariantId:ci.variant?.id ?? 0,
                productId: ci.id,
                IngredientsId: (ci.ingredients?.map(i => i.id) ?? []).flat(),
                quantity: co.quantity
              }
            }),
            comboId:co.id,
            quantity:co.id//:ComboMinimalresponseDto
          },
          comboId:co.id,
          quantity:co.quantity
        }
      })
    }

      console.log('after mapping of ceckOutCombos '+checkOutCombos)
      console.log(JSON.stringify(checkOutCombos))
      
      
      //assemble complete order from local variables
      let order: CreateOrderDto = {
        customerNote: comment,
        orderComboDtos: checkOutCombos,
        orderProductDtos: checkOutProducts
        }
    
      console.log(this.cart)
      console.log(order)
      console.log(JSON.stringify(order));

    
      const receipt = fetcher.createOrder(order);
      console.log(receipt)

      // empty cart for next order
      this.clearCart()
    }
    
  }
})
