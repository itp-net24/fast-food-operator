import {Product} from '@/models/product'

export interface Cart{
    cartProducts:
    Array<cartProduct>;
}

export interface cartProduct{
        cartProduct:Product,
        qty:number
    }


export interface State{
    cart: Cart;
}