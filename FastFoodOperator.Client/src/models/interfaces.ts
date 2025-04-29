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
export interface OrderProductDto{
      productId: number,
      quantity: number
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
     OrderProductDto
    ]
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