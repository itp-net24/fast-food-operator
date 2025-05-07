import type { CartContainer, CartItem } from '@/models/types'

// export interface Cart{
//     cartProducts:
//     Array<CartContainer>,
//     cartCombos:
//     Array<CartContainer>
// }

export interface State{
    cart: CartContainer[],
    receipt?:Receipt | null
}

export interface Receipt {
  products: [
    {
      productName: string,
      productPrice: number,
      quantity: number
    }
  ],
  combos:
    [
      {
        comboName: string,
        comboPrice: number,
        quantity: number
      }
    ],
  createdAt: Date,
  orderNumber: number,
  totalVatSixPercent: number,
  totalVatTwelvePercent: number,
  totalVatTwentyFivePercent: number,
  totalTax: number,
  orderFinalPrice: number
}

export interface Tag{
  id: number;
  name: string;
  taxrate: number;
}



export interface OrderProductDtos{
  productMinimalResponseDto: ProductMinimalResponseDto
}





export interface CreateOrderDto {
    customerNote: string;
    orderComboDtos?: OrderComboDtos[];
    orderProductDtos?: OrderProductDtos[]; // orderProductDtos
  }








export interface ProductMinimalResponseDto{
  productVariantId:number,
  productId:number,
  ingredientIds:number[],
  quantity:number
}

export interface ComboMinimalResponseDto {

  products:ProductMinimalResponseDto[]
  comboId: number;
  quantity: number;
}

export interface OrderComboDtos{
  comboMinimalResponseDto:ComboMinimalResponseDto,
  comboId:number,
  quantity:number
}
