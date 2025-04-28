import {defineStore} from "pinia";
import {Product} from '@/models/product'
import type {Cart, cartProduct, State} from '@/models/interfaces'


export const useCartStore = defineStore('cart',{
    state: ():State => ({
        cart:{}
    }),
    actions:{
        loadCartInstance(){
            const cs = localStorage.getItem('cart')
            if(!cs)
            this.cart = {}
            else
            this.cart = JSON.parse(cs)
        },

        addToCart(pro:Product){
            const cs = localStorage.getItem('cart')

            let isAdded = false

            if(!cs)
            {
                this.cart = {
                    cartProducts:[
                        {product:pro,qty:1}
                    ]
                }
            }
            else{
                let cartLocalStorage = JSON.parse(cs)



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
        }
    }
})