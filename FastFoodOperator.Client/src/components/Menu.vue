<script setup lang="ts">
    import Sidebar from './Sidebar.vue'
    import ProductCard from './ProductCard.vue'
    import {ref, onMounted} from 'vue'
    import {Product} from '@/models/product.ts'
    import Fetcher from "@/ApiFetcher.ts"
    
    const fetcher = new Fetcher();
    const products = ref<Product[] | null>([]);

    onMounted(async () => {
  try {
    products.value = await fetcher.getProducts();

  } catch (err) {
    console.error('error:', err);
  }
})

</script>

<template>
    <div class="menu-container">
        
            <!-- <aside>
                <Sidebar />
            </aside> -->

            <main>
              
              <button id="cartButton"> Cart </button>
              <div id="cartModal" class="modal">
                <div class="modal-content">
                  <span class="close">&times;</span>
                  <div v-for="product in cart"></div>
                </div>
              </div>

                <div v-for="product in products" :key="product.id" class="articles-container">
                    <ProductCard :product="product" />
                </div>
            </main>
        
    </div>
</template>

<style scoped>
.menu-container {
  padding-top: 1rem;
}

main {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
}
</style>