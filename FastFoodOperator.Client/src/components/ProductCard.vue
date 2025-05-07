<script setup lang="ts">
import { defaultProductOfGroup, isProductCombo } from '@/utils/helpers.ts'
import { GetComboAsync, GetProductAsync } from '@/services/fetcher.ts'
import { mapComboProductToCart, mapProductToCart, mapToCartContainer } from '@/utils/mappers.ts'
import type { BaseProduct, CartContainer } from '@/models/types.ts'

const props = defineProps<Props>()

interface Props {
  baseProduct: BaseProduct
}

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

  // Logic to add to cart goes here!
  console.log(productToAdd);
}
</script>

<template>
  <article class="border-menu">
    <img id="product-image" v-if="baseProduct.imageUrl" :src="baseProduct.imageUrl" :alt="`image of ${baseProduct.name}`" />

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

  background-color: white;
}

h2 {
  padding: var(--spacing-xs);
  min-height: 60px;
}

#product-image {
  height: 256px;
  width: 256px;
  background-repeat: no-repeat;
  background-size: cover;
}
</style>
