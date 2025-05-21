<template>
  <div class="orders-section">
    <h2>{{ title }}</h2>
    <div v-if="orders.length === 0">No orders in this section</div>

    <OrderCard
      v-for="order in orders"
      :key="order.orderId"
      :composable="composable"
      :order="order"
      :status="status"
    />
  </div>
</template>

<script setup lang="ts">
import OrderCard from './OrderCard.vue';
import type { OrdersComposable } from '@/composables/useOrders.ts'
import { computed } from 'vue'

const props = defineProps<Props>();

interface Props {
  composable: OrdersComposable;
  status: number;
  title: string;
}

const orders = computed(() => props.composable.orders.value.filter(o => o.orderStatus === props.status));
</script>
