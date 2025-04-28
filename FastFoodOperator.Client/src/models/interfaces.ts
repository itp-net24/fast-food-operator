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
  priceModifier: number;
}
  
export interface OrderProductDto {
  productId: number;
  quantity: number;
  productVariant: string; 
  productVariantPriceModifier: number; 
  productIngredients: string[]; 
}
  
export interface CreateOrderDto {
    customerNote: string;
    orderComboDtos?: OrderComboDto[];  
    orderProductDtos?: OrderProductDto[];  
  }
  