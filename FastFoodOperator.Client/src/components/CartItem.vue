<script setup lang="ts">
import {useCart} from '@/stores/testCart.ts'
import {storeToRefs} from 'pinia'
import type {CartContainer} from '@/models/types'

interface Props {
  cartProduct:CartContainer
}
const props = defineProps<Props>()

const cartStore = useCart();
const {cart} = storeToRefs(cartStore)

function addToCart(){
  cartStore.addToCart(props.cartProduct);
  console.log("cart", cart.value)
}

function removeFromCart(){
  cartStore.removeFromCart(props.cartProduct)
  console.log('removing from cart')
}
</script>

<template>
  <article>
    <div id="product-image" :style="{ backgroundImage: 'url(' + (cartProduct && cartProduct.product.imageUrl? cartProduct.product.imageUrl: '') + ')' }">
            <span>
               Quantity: {{ cartProduct.qty }}
               Price each : {{ cartProduct.product.basePrice }}
               Price Total : {{ cartProduct.product.basePrice * cartProduct.qty }}
            </span>
    </div>
    <h2> {{ cartProduct.product?.name }}</h2>

    <span>
        <button class="button-basic" @click="decrementFromCart">- 1</button>
        <button class="button-basic" @click="addToCart">+ 1</button>
        </span>
    <button class="button-basic" @click="removeFromCart">Remove</button>
  </article>
</template>

<style scoped>
* {
  margin: 0;
  margin-top: 0;
}
article {
  width: 270px;
  height: 100%;
  display:flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  gap: 4px;

  border: 2px solid rgba(182, 181, 181, 0.613);
  border-radius: 4px;

  transition: border-color 0.3s ease;
}

article:hover {
  border-color: gray;
}

h2 {
  padding: var(--spacing-xs);
  min-height: 60px;
}

button {
  padding: 0.6rem 1.2rem;
  margin-bottom: var(--spacing-xs);
  background-color: var(--color-primary);
  color: white;
  font-weight: 550;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-size: 1rem;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

button:hover {
  background-color: var(--color-hover);
  transform: scale(1.05);
}

#product-image {
  height: 256px;
  width: 256px;
  background-repeat: no-repeat;
  background-size: cover;
}
</style>
