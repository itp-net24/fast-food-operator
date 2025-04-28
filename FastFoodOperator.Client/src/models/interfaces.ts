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
    cart: Cart | {cartProducts:[]}
}

export interface OrderDTO{
    customerNote: string,
    orderComboDtos: [
      {
        comboId: number,
        quantity: number
      }
    ],
    orderProductDtos: [
      {
        productId: number,
        quantity: number
      }
    ]
  }