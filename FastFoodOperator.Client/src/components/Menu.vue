<script setup lang="ts">
import Sidebar from './Sidebar.vue'
import ProductCard from './ProductCard.vue'
import ScrollToTop from './ScrollToTop.vue'
import { onMounted, ref } from 'vue'
import CartModal from './CartModal.vue'
import PopupMessenger from './PopupMessenger.vue'
import { getProductsAsync, getProductsByTagAsync } from '@/services/fetcher.ts'
import type { BaseProduct } from '@/models/types.ts'
import ProductModal from '@/components/productWindow/ProductModal.vue'
import { ProductType } from '@/enums/enums.ts'
import { isProductCombo } from '@/utils/helpers.ts'

const products = ref<BaseProduct[]>([])
const showPopupMessage = ref<boolean>(false);

onMounted(async () => {
  products.value = await getProductsAsync(20, 0);
})

async function handleCategoryChanged(tagId: number) {
  products.value = await getProductsByTagAsync(tagId);
}

const visible = ref<boolean>(false);
const selectedProduct = ref<BaseProduct | null>(null);

const handleCardClick = (product: BaseProduct) => {
  visible.value = true;
  selectedProduct.value = product;
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

    <PopupMessenger 
    class="add-to-cart-popup"
    message="Item added to cart."
    v-bind:show="showPopupMessage"
    v-on:hide="() => showPopupMessage = false"/>
  </div>

  <div class="menu-container">
    <aside>
      <Sidebar @category-clicked="handleCategoryChanged" />
    </aside>

    <main>
      <div v-for="product in products" :key="product.id" class="articles-container">
        <ProductCard 
        :baseProduct="product" 
        @click="handleCardClick(product)" 
        v-on:cart="() => showPopupMessage = true"/>
      </div>
    </main>
  </div>
  <CartModal />

  <ScrollToTop/>
  
</template>

<style scoped>
.menu-container {
  padding-top: 1rem;
  display: flex;
}

main {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
}

.add-to-cart-popup {
  position: fixed;
  top: 0;
  right: 0;
  z-index: 800;

  margin: 0.5rem;
  padding: 1rem;

  color: rgba(27, 194, 27, 0.817);
  text-shadow:
    -1px -1px 0 white,
     1px -1px 0 white,
    -1px  1px 0 white,
     1px  1px 0 white;
}

@media (max-width: 640px) {

  .articles-container {
    flex-basis: 45%;
    margin: 0 auto;
  }
}
</style>
