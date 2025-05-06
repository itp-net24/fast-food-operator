<script setup lang="ts">
import Sidebar from './Sidebar.vue'
import ProductCard from './ProductCard.vue'
import { onMounted, ref } from 'vue'
import CartModal from './CartModal.vue'
import { getProductsAsync, getProductsByTagAsync } from '@/services/fetcher.ts'
import type { BaseProduct } from '@/models/types.ts'

const products = ref<BaseProduct[]>([]);

onMounted(async () => {
  products.value = await getProductsAsync(20, 0);
})

async function onCategoryClicked(tagId: number) {
  products.value = await getProductsByTagAsync(tagId, 20, 0);
}
</script>

<template>
  <div class="company-title">
    <img src="@/assets/Claes_Burgir1.png" alt="Company logo" class="company-logo">
  </div>

  <div class="menu-container">
    <aside>
      <Sidebar v-on:category-clicked="onCategoryClicked"/>
    </aside>

    <main>
      <div v-for="product in products" :key="product.id" class="articles-container">
        <ProductCard :baseProduct="product" />
      </div>
    </main>
  </div>
  <CartModal/>
</template>

<style scoped>
.menu-container {
  padding-top: 1rem;
}

.menu-container {
  display: flex;
}

main {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
}

@media (max-width: 640px)
{
  main {
    justify-content: center;
  }
}
</style>
