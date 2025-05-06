<script setup lang="ts">
import Sidebar from './Sidebar.vue'
import ProductCard from './ProductCard.vue'
import { onMounted, ref } from 'vue'
import CartModal from './CartModal.vue'
import { getProductsAsync, getProductsByTagAsync } from '@/services/fetcher.ts'
import type { BaseProduct } from '@/models/types.ts'
import ProductModal from '@/components/productWindow/ProductModal.vue'
import { ProductType } from '@/enums/enums.ts'
import { isProductCombo } from '@/utils/helpers.ts'

const products = ref<BaseProduct[]>([])

onMounted(async () => {
  products.value = await getProductsAsync(20, 0)
})

async function onCategoryClicked(tagId: number) {
  products.value = await getProductsByTagAsync(tagId)
}

const visible = ref<boolean>(false)
const selectedProduct = ref<BaseProduct | null>(null)

const handleCardClick = (product: BaseProduct) => {
  visible.value = true
  selectedProduct.value = product
}
</script>

<template>
  <ProductModal
    v-if="selectedProduct"
    :id="selectedProduct.id"
    :type="isProductCombo(selectedProduct) ? ProductType.combo : ProductType.product"
    :visible="visible"
    @close="() => (visible = false)"
  />
  <div class="company-title">
    <img src="@/assets/Claes_Burgir1.png" alt="Company logo" class="company-logo" />
  </div>

  <div class="menu-container">
    <aside>
      <Sidebar v-on:category-clicked="onCategoryClicked" />
    </aside>

    <main>
      <div v-for="product in products" :key="product.id" class="articles-container">
        <ProductCard :baseProduct="product" @click="handleCardClick(product)" />
      </div>
    </main>
  </div>
  <CartModal />
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

@media (max-width: 640px) {
  main {
    justify-content: center;
  }
}
</style>
