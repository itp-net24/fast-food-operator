import {Product} from '@/models/product'

export interface Cart{
    cartProducts:
    Array<cartProduct>
}

export interface cartProduct{
        product:Product,
        qty:number
    }


export interface State{
    cart: Cart | {}
}