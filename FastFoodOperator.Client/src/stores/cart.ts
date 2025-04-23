import {defineStore} from "pinia";
import {Product} from '@/models/product'

interface Cart{
  //  cid: string
    products:Array<{id: number, qty: number}>
}

// interface Product{
//     id:number,
//     qty:number
// }


interface State{
    cart: Cart | {}
}


export const useCartStore = defineStore('cart',{
    state: () => ({cart: {}} as State),
    actions:{
        loadCartInstance(){
            const cs = localStorage.getItem('cart')
            if(!cs)
            {
                this.cart = {}
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
                    products:[
                        product
                    ]
                }
            }
            else{
                let cartLocalStorage = JSON.parse(cs)

                cartLocalStorage.products = cartLocalStorage.products.map((ci: Product) => {
                    if(ci.id == product.id)
                    {
                        isAdded = true
                        return{id: ci.id}
                    }
                    else
                    {
                    return {id: ci.id }
                    }
                })

                if(!isAdded)
                {
                    cartLocalStorage.products.push({id: product.id})
                }

                this.cart = cartLocalStorage

            }

            localStorage.setItem('cart',JSON.stringify(this.cart))


        },

        removeFromCart(id:number){
            (this.cart as Cart).products = (this.cart as Cart).products.filter(ci => ci.id != id)
            localStorage.setItem('cart',JSON.stringify(this.cart))
        }
    }
})