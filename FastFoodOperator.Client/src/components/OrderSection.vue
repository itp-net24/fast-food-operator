<template>
  <div class="orders-section">
    <h2>{{ title }}</h2>
    <div v-if="orders.length === 0">Inga {{ title.toLowerCase() }}</div>

    <OrderCard
      v-for="order in orders"
      :key="order.orderId"
      :order="order"
      :status="status"
      @start="$emit('start', order)"
      @complete="$emit('complete', order)"
    />
  </div>
</template>

<script>
import OrderCard from './OrderCard.vue';

export default {
  components: { OrderCard },
  props: {
    orders: Array,
    status: Number,
    title: String,
  }
}
</script>

<style scoped>
.orders-section {
  margin-bottom: 1.5rem;
}
</style>
