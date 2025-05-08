<template>
  <div class="orders-section">
    <h2>{{ title }}</h2>
    <div v-if="orders.length === 0">No orders in this section</div>

    <OrderCard
      v-for="order in orders"
      :key="order.orderId"
      :order="order"
      :status="status"
      @start="$emit('start', order)"
      @complete="$emit('complete', order)"
      @delete="$emit('delete', order)"
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
  },
};
</script>
