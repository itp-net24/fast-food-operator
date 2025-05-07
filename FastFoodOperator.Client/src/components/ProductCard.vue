<script setup lang="ts">
import { defaultProductOfGroup, isProductCombo } from '@/utils/helpers.ts'
import { GetComboAsync, GetProductAsync } from '@/services/fetcher.ts'
import { mapComboProductToCart, mapProductToCart, mapToCartContainer } from '@/utils/mappers.ts'
import type { BaseProduct, CartContainer } from '@/models/types.ts'
import {useCartStore} from '../stores/cart'
import {storeToRefs} from 'pinia'
import {onMounted, defineEmits} from 'vue'


const props = defineProps<Props>()

interface Props {
  baseProduct: BaseProduct
}

const cartStore = useCartStore()
const {cart} = storeToRefs(cartStore)

onMounted(() =>{
  cartStore.loadCartInstance()
})

const emit = defineEmits<{
  (e: 'cart'):void
}>();


const addToCart = async (): Promise<void> => {
  if (!props.baseProduct) return;

  let productToAdd: CartContainer;
  if (isProductCombo(props.baseProduct as BaseProduct)) {
    const combo = await GetComboAsync(props.baseProduct.id);

    const items = combo.comboProducts.map(cp => mapComboProductToCart(cp, false));
    combo.comboGroups.forEach(group => items.push(mapComboProductToCart(defaultProductOfGroup(group), false)));

    productToAdd = mapToCartContainer(combo, items);
  }
  else {
    const product = await GetProductAsync(props.baseProduct.id);
    const item = mapProductToCart(product);
    productToAdd = mapToCartContainer(product, [item]);
  }

  emit('cart'); // Kod till popupmessenger

  // Logic to add to cart goes here!
  console.log(productToAdd);
  cartStore.addToCart(productToAdd);
  console.log("cart", cart.value)

}
</script>

<template>
  <article class="border-menu popout">
    <img id="product-image" class="popout" v-if="baseProduct.imageUrl" :src="baseProduct.imageUrl" :alt="`image of ${baseProduct.name}`" />

    <h2> {{ baseProduct.name }}</h2>

    <button class="button-basic button-menu" @click.stop="addToCart">Add to Cart</button>
  </article>
</template>

<style scoped>
article {
  width: 270px;
  height: 100%;
  display:flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  gap: 4px;
  cursor: pointer;
    overflow:hidden;
    background-color: white;
  background-color: white;
}

h2 {
  padding: var(--spacing-xs);
  min-height: 60px;
  
}

#product-image {
  max-height: 256px;
  max-width: 256px;
  width: 100%;
  height: auto;
  aspect-ratio: 1;
  box-shadow: none;
}

@media (max-width: 640px)
{
  article {
    width: auto;
  }
  
  h2 {
    overflow: hidden;
  }
}
</style>
