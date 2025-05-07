import {Product} from '@/models/product'
import type{CartContainer} from '@/models/types'

export interface Cart{
    cartProducts:
    Array<CartContainer>,
    cartCombos:
    Array<CartContainer>
}



export interface State{
    cart: Cart | {cartProducts:[],cartCombos:[]}
}

export interface Tag{
  id: number;
  name: string;
  taxrate: number;
}


export interface OrderComboDtos{
  comboMinimalResponseDto:ComboMinimalResponseDto,
  comboId:number,
  quantity:number
}

export interface OrderProductDtos{
  productMinimalResponseDto: ProductMinimalResponseDto  
}

export interface ProductMinimalResponseDto{
  productVariantId:number,
  productId:number,
  IngredientsId:number[],
  quantity:number
}


export interface ComboMinimalResponseDto {
  
  products:ProductMinimalResponseDto[]
  comboId: number;
  quantity: number;
}


export interface CreateOrderDto {
    customerNote: string;
    orderComboDtos?: OrderComboDtos[];
    orderProductDtos?: OrderProductDtos[];
  }
