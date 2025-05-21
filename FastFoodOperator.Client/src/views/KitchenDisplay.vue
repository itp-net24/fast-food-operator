<script setup lang="ts">
import '@/assets/kitchenDisplay.css';
import OrderSection from '../components/OrderSection.vue';
import { onMounted } from 'vue'
import useOrders from '@/composables/useOrders.ts'

const kitchen = useOrders();

const showCompleted = true;

onMounted(async () => {
  await kitchen.initializeAsync();
});
</script>

<template>
  <div class="kitchen-container">
    <h1 class="title-prim-color">Kitchen Display</h1>

    <div v-if="kitchen" class="orders-wrapper">
      <!-- Ej påbörjade ordrar -->
      <OrderSection
        :composable="kitchen"
        :status="0"
        title="Not started orders"
      />

      <!-- Pågående ordrar -->
      <OrderSection
        :composable="kitchen"
        :status="1"
        title="Not started orders"
     />

      <!-- Färdiga ordrar -->
      <OrderSection
        v-if="showCompleted"
        :composable="kitchen"
        :status="2"
        title="Completed"
        />
    </div>
  </div>
</template>
