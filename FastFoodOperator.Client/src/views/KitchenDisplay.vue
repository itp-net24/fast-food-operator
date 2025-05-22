<script setup lang="ts">
import '@/assets/kitchenDisplay.css';
import OrderSection from '../components/OrderSection.vue';
import { orderStore } from '@/stores/orderStore.ts'
import { onMounted } from 'vue'

const store = orderStore();

const showCompleted = true;

onMounted(async() => store.init());
</script>

<template>
  <div class="kitchen-container">
    <h1 class="title-prim-color">Kitchen Display</h1>

    <div class="orders-wrapper">
      <!-- Ej påbörjade ordrar -->
      <OrderSection
        :status="0"
        title="Not started orders"
      />

      <!-- Pågående ordrar -->
      <OrderSection
        :status="1"
        title="Not started orders"
     />

      <!-- Färdiga ordrar -->
      <OrderSection
        v-if="showCompleted"
        :status="2"
        title="Completed"
        />
    </div>
  </div>
</template>
