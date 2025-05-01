import {Product} from '@/models/product'
import type{ProductToCart,ComboToCart} from '@/models/types'

export interface Cart{
    cartProducts:
    Array<ProductToCart>
}

export interface cartProduct{
        product:Product,
        qty:number
    }


export interface State{
    cart: Cart | {cartProducts:[]}
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
  
