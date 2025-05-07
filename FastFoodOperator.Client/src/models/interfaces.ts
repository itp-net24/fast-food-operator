import {Product} from '@/models/product'
import type{CartContainer,CartItem} from '@/models/types'

export interface Cart{
    cartProducts:
    Array<CartContainer>,
    cartCombos:
    Array<CartContainer>
}

// export interface CartContainer {
//   id: number;
//   type: string;
//   imageUrl: string | null;
//   name: string;
//   tags: Tag[];
//   price: number;
//   quantity: number;
//   products: CartItem[];
// }

// export interface CartItem {
//   __uid: number;
//   id: number;
//   name: string;
//   tax: number;
//   basePrice: number;
//   variant?: Variant | null;
//   ingredients?: Ingredient[] | null;
// }


export interface State{
    cart: Cart | {cartProducts:[],cartCombos:[]}
}

export interface Tag{
  id: number;
  name: string;
  taxrate: number;
}


export interface AddOrderDTO
    {
        customerNote: string,
        orderComboDtos: [
          {
            comboMinimalResponseDto: {
              products: [
                {
                  productVariantId: number,
                  productId: number,
                  ingredientsId: [
                    number
                  ],
                  quantity: number
                }
              ],
              comboId: number,
              quantity: number
            }
          }
        ],
        orderProductDtos: [
          {
            productMinimalResponseDto: {
              productVariantId: number,
              productId: number,
              ingredientsId: [
                number
              ],
              quantity: number
            }
          }
        ]
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
