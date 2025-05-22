<template>
  <div class="orders-section">
    <h2>{{ title }}</h2>
    <div v-if="orders.length === 0">No orders in this section</div>

    <OrderCard
      v-else
      v-for="order in orders"
      :key="order.orderId"
      :order="order"
      :status="status"
    />
  </div>
</template>

<script setup lang="ts">
import OrderCard from './OrderCard.vue';
import { orderStore } from '@/stores/orderStore.ts'
import { computed } from 'vue'

const store = orderStore();

const props = defineProps<Props>();

interface Props {
  status: number;
  title: string;
}

const orders = computed(() => store.orders.filter(o => o.orderStatus === props.status));
</script>
