<script setup lang="ts">
import '@/assets/DisplayOrderNumber.css';
import { orderStore } from '@/stores/orderStore.ts'
import { computed, onMounted } from 'vue'

const store = orderStore();
const activeOrderNumbers = computed(() => store.orders.filter(o => o.orderStatus !== 2).map(o => o.orderId));
const completedOrderNumbers = computed(() => store.orders.filter(o => o.orderStatus === 2).map(o => o.orderId));

onMounted(async() => store.init());
</script>

<template>
  <div class="display-container">
    <div class="company-title">
      <img src="@/assets/Claes_Burgir1.png" alt="Företagslogotyp" class="company-logo">
    </div>

    <div class="display-box">
      <h2>Preparing</h2>
      <div class="number-display">
        <p class="paragraph" v-if="activeOrderNumbers.length > 0" v-for="(num, index) in activeOrderNumbers" :key="index">
          #{{ num }}
        </p>
        <p class="paragraph" v-else>No Active Orders</p>
      </div>
    </div>

    <div class="completed-orders-box">
      <h2>Ready For Pickup</h2>
      <div class="number-display">
        <p
          v-for="(order, index) in completedOrderNumbers"
          :key="'ready-' + index"
          class="completed-highlight paragraph"
        >
          #{{ order }}
        </p>
      </div>
    </div>
  </div>
</template>
