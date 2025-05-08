<script setup lang="ts">
import { defaultProductOfGroup, isProductCombo } from '@/utils/helpers.ts'
import { GetComboAsync, GetProductAsync } from '@/services/fetcher.ts'
import { mapComboProductToCart, mapProductToCart, mapToCartContainer } from '@/utils/mappers.ts'
import type { BaseProduct, CartContainer } from '@/models/types.ts'
import {useCart} from '@/stores/testCart.ts'


const props = defineProps<Props>()

interface Props {
  baseProduct: BaseProduct
}

const cartStore = useCart();

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


  cartStore.addToCart(productToAdd);

}
</script>

<template>
  <article class="border-menu popout">
    <img id="product-image" class="popout" v-if="baseProduct.imageUrl" :src="baseProduct.imageUrl" :alt="`image of ${baseProduct.name}`" />

    <h2> {{ baseProduct.name }}</h2>

    <div class="lower-box">
      <span class="product-price"> {{ baseProduct.basePrice + 'kr' }}</span>
      <button class="button-basic button-menu" @click.stop="addToCart">Add to Cart</button>
    </div>
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

.lower-box {
  display: flex;
  justify-content: space-evenly;
  align-items: center;
}

.product-price {
  font-size: 1.3rem;
  font-weight: 600;
}

@media (max-width: 640px)
{
  .lower-box {
    flex-wrap: wrap;
  }
  article {
    width: auto;
  }

  h2 {
    overflow: hidden;
  }
}
</style>
