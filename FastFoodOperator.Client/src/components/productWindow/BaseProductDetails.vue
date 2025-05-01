<script setup lang="ts">
  import type {BaseProduct} from "@/models/types.ts";

  const props = defineProps<Props>();

  interface Props {
    totalPrice: number;
    baseProduct: BaseProduct
  }
</script>

<template>
  <div class="product-container">
    <img v-if="baseProduct.imageUrl" class="product-image" :src="baseProduct.imageUrl" alt="image of {{baseProduct.name}}" />

    <div class="product-content">
      <h2 class="product-name">{{baseProduct.name}}</h2>
      <span class="product-price">{{ totalPrice + 'kr' }}</span>
    </div>
  </div>
</template>

<style scoped>
.product-image {
  width: 100%;
  height: 15rem;
  object-fit: cover;
  display: block;
}

.product-content {
  display: flex;
  justify-content: space-between;
  align-items: center;

  margin-bottom: 1rem;
}

.product-name {
  font-size: 1.5rem;
  font-weight: 600;
}

.product-price {
  font-size: 1.3rem;
  font-weight: 600;
}
</style>