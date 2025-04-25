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

export interface OrderComboDto {
    comboId: number;
    quantity: number;
  }
  
export interface OrderProductDto {
    productId: number;
    quantity: number;
  }
  
export interface CreateOrderDto {
    customerNote: string;
    orderComboDtos?: OrderComboDto[];  
    orderProductDtos?: OrderProductDto[];  
  }
  