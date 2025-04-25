import {defineStore} from "pinia";
import {Product} from '@/models/product'
import type {Cart, cartProduct, State} from '@/models/interfaces'


export const useCartStore = defineStore('cart',{
    state: ():State => ({
        cart:{
            cartProducts:[]
        }
    }),
    actions:{
        loadCartInstance(){
            const cs = localStorage.getItem('cart')
            if(!cs)
            {
                this.cart = {cartProducts:[]}
            }
            else
            {
                this.cart = JSON.parse(cs)
            }
        },

        addToCart(product:Product){
            const cs = localStorage.getItem('cart')

            let isAdded = false

            if(!cs)
            {
                this.cart = {
                    cartProducts:[
                        {cartProduct:product,qty:1}
                    ]
                }
            }
            else{
                let cartLocalStorage = JSON.parse(cs)



                cartLocalStorage.cartProducts = cartLocalStorage.cartProducts.map((ci: cartProduct) => {
                    if(ci.cartProduct.id == product.id)
                    {
                        isAdded = true
                        return{cartProduct:product, qty:ci.qty + 1 }
                    }
                    else
                    {
                    return {cartProduct:product, qty:ci.qty}
                    }
                })

                if(!isAdded)
                {
                    cartLocalStorage.cartProducts.push({product})
                }

                this.cart = cartLocalStorage

            }

            localStorage.setItem('cart',JSON.stringify(this.cart))


        },

        removeFromCart(product:Product){
            (this.cart as Cart).cartProducts = (this.cart as Cart).cartProducts.filter(ci => ci.cartProduct.id != product.id)
            localStorage.setItem('cart',JSON.stringify(this.cart))
        }
    }
})