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