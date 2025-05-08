<script setup lang="ts">
import {useCart} from '@/stores/testCart.ts'
import type {CartContainer} from '@/models/types'
import ValueSelector from '@/components/ValueSelector.vue'
import { storeToRefs } from 'pinia'
import { computed } from 'vue'
import { CURRENCY_SYMBOL } from '../../config.ts'

interface Props {
  product: CartContainer
}

const props = defineProps<Props>()

const cartStore = useCart();
const { cart } = storeToRefs(cartStore);

const cartProduct = computed(() => {
  return cart.value.find(p => p.id === props.product.id)!;
});

function addToCart(){
  cartStore.addToCart(props.product);
}

function removeFromCart(){
  cartStore.removeFromCart(props.product)
}
</script>

<template v-if="cartProduct">
  <article class="item-container">
    <img :src="product.imageUrl!" />

    <div class="item-wrapper">
      <div class="item-header">
        <h2> {{ product.name }}</h2>
        <button class="remove-button" @click="removeFromCart">X</button>
      </div>
      <div class="item-price">
        <p>{{ product.price + CURRENCY_SYMBOL }}</p>
        <p>x {{ product.quantity }}</p>
      </div>
      <ValueSelector v-model:value="cartProduct.quantity" :min="1" :max="99" :step="1" />
    </div>

  </article>
</template>

<style scoped>
.item-container {
  display: flex;
  flex-direction: row;
  justify-content: start;
  align-items: center;
}

img {
  width: 128px;
  height: 128px;
}

.item-wrapper {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: start;
}

.remove-button {
  font-size: 0.7rem;
  width: 1rem;
  aspect-ratio: 1/1;
  border: 1px solid black;
  border-radius: 50%;
  float: right;
}

.item-header {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}

.item-price {
  display: flex;
  justify-content: space-between;
  width: 100%;
}
</style>
