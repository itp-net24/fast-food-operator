import {defineStore} from "pinia";
import {Product} from '@/models/product'
import type {Cart, cartProduct, State, AddOrderDTO, OrderDTO,OrderProductDtos} from '@/models/interfaces'
import fetcher from '@/ApiFetcher'


export const useCartStore = defineStore('cart',{
    state: ():State => ({
        cart:{cartProducts:[]}
    }),
    actions:{
        loadCartInstance(){
            const cs = localStorage.getItem('cart')
            if(!cs)
            this.cart = {cartProducts:[]}
            else
            this.cart = JSON.parse(cs)
        },

        addToCart(pro:Product){
            console.log('start of addToCart')
            const cs = localStorage.getItem('cart')

            let isAdded = false

            console.log(!cs)
            if(!cs)
            {
                this.cart = {
                    cartProducts:[
                        {product:pro,qty:1}
                    ]
                }
            }
            else{

                console.log(localStorage.getItem('cart'))

                let cartLocalStorage = JSON.parse(cs)
                console.log(cartLocalStorage)
                cartLocalStorage.cartProducts = cartLocalStorage.cartProducts.map((ci: cartProduct) => {
                    if(ci.product.id === pro.id)
                    {
                        isAdded = true
                        return{product:pro, qty:ci.qty + 1 }
                    }    
                    return {product:ci.product, qty:ci.qty}  
                })

                if(!isAdded)
                {
                    cartLocalStorage.cartProducts.push({product:pro,qty:1})
                }

                this.cart = cartLocalStorage

            }

            
            localStorage.setItem('cart',JSON.stringify(this.cart))


        },

        removeFromCart(pro:Product){
            (this.cart as Cart).cartProducts = (this.cart as Cart).cartProducts.filter(ci => ci.product.id != pro.id)
            localStorage.setItem('cart',JSON.stringify(this.cart))
        },

        displayCartLoad(){},

        clearCart()
        {
        (this.cart as Cart) = {cartProducts:[]}
        localStorage.setItem('cart',JSON.stringify(this.cart))
        },

        checkOut()
        {

            //set up array for orderProductDtos
            let orderProductDtos = []
            //map all products in cart to orderProductDtos
            orderProductDtos = (this.cart as Cart).cartProducts.map((ci: cartProduct) =>{
                return{
                    productId: ci.product.id,
                    quantity: ci.qty
                  }
            })

            //verify that orderProductDtos were mapped correctly
            console.log(orderProductDtos)

            //set up array for orderComboDtos
            // let orderComboDtos = []
            // orderComboDtos = (this.cart as Cart).cartOrders.map((co:cartOrder) =>{
            //     return {
            //         comboId: co.order.id,
            //         quantity:co.qty
            //       }
            // })
            // console.log('array of ordercomboDtos '+orderComboDtos)

            let order:OrderDTO = {
                customerNote: 'Gabriel intial testing to learn',
                orderComboDtos: [
                  {
                    comboId: 0,
                    quantity: 0
                  }
                ],
                orderProductDtos:orderProductDtos
              }

            this.createOrder(order)

            // empty cart for next order
           this.clearCart()
            
        },

        async createOrder(order: OrderDTO) : Promise<any> {
            try {
                const baseURL = 'https://localhost:8080/api/'
              const response = await fetch((`${baseURL}order/add`), {
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
})