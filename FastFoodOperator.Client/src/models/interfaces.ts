import {Product} from '@/models/product'
import type{CartContainer,CartItem} from '@/models/types'

export interface Cart{
    cartProducts:
    Array<CartContainer>,
    cartCombos:
    Array<CartContainer>
}

// ToCart interfaces
// export interface CartContainer {
//   id: number;
//   type: string;
//   imageUrl: string | null;
//   name: string;
//   price: number;
//   quantity: number;
//   products: CartItem[];
// }

// export interface CartItem {
//   __uid: number;
//   id: number;
//   name: string;
//   variant?: Variant | null;
//   ingredients?: Ingredient[] | null;
// }


export interface State{
    cart: Cart | {cartProducts:[],cartCombos:[]}
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
