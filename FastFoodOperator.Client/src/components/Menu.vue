<script setup lang="ts">
    import Sidebar from './Sidebar.vue'
    import ProductCard from './ProductCard.vue';
    import {ref, onMounted} from 'vue'
    import {Product} from '@/models/product.ts'
    
    const products = ref<Product[]>([]);

    onMounted(async () => {
  try {
    const response = await fetch('')
    if (!response.ok) throw new Error('Failed to fetch')
    products.value = await response.json()
  } catch (err) {
    console.error('Fetch error:', err)
  }
})

</script>

<template>
    <div class="menu-container">
        
            <aside>
                <Sidebar />
            </aside>

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

</style>