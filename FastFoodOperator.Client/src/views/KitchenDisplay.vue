<template>
  <div class="kitchen-container">
    <h1 class="title-prim-color">Kitchen Display</h1>

    <div class="orders-wrapper">
      <!-- Ej påbörjade ordrar -->
      <OrderSection
        :orders="pendingOrders"
        :status="0"
        title="Not started orders"
        @start="markOrderAsInProgress"
        @delete="deleteOrder"
      />

      <!-- Pågående ordrar -->
      <OrderSection
        :orders="activeOrders"
        :status="1"
        title="In progress orders"
        @complete="markOrderAsComplete"
        @delete="deleteOrder"
      />

      <!-- Färdiga ordrar -->
      <OrderSection
        v-if="showCompleted"
        :orders="completedOrders"
        :status="2"
        title="Finished orders"
        @delete="deleteOrder"
      />
    </div>
  </div>
</template>

<script>
import '@/assets/kitchenDisplay.css';
import OrderSection from '../components/OrderSection.vue';
import { StartOrder, CompleteOrder, DeleteOrder, GetOrders } from '@/services/fetcher';


export default {
  components: { OrderSection },
  data() {
    return {
      orders: [],
      intervalId: null,
      showCompleted: true,
    };
  },

  computed: {
    pendingOrders() {
      console.log('pedning Orders:', this.orders.filter(order => order.orderStatus === 0));
      return this.orders.filter(order => order.orderStatus === 0); // Ej påbörjade ordrar
    },
    activeOrders() {
      console.log('Active Orders:', this.orders.filter(order => order.orderStatus === 1));
      return this.orders.filter(order => order.orderStatus === 1); // Pågående ordrar
    },
    completedOrders() {
      console.log('Active Orders:', this.orders.filter(order => order.orderStatus === 2));
      return this.showCompleted
        ? this.orders.filter(order => order.orderStatus === 2) //Färdiga ordrar
        : [];
    }
  },
  methods: {



    // Hämtar ordrar från API
    async fetchOrders() {
      try {
        const orders = await GetOrders();
        this.orders = orders
      } catch (error) {
        console.error('Error fetching orders:', error);
      }
    },

    // Markera order som påbörjad
    async markOrderAsInProgress(order) {
      try {
        await StartOrder({ orderId: order.orderId });
        await this.fetchOrders();
      } catch (error) {
        console.error('Fel vid uppdatering av order:', error);
      }
    },
    //Radera Order

    async deleteOrder(order) {
      if (!confirm(`Are you sure you want to delete #${order.orderNumber}?`)) return;

      try {
        await DeleteOrder(order.orderId);
        // Uppdatera listan efter borttagning
        await this.fetchOrders();

      } catch (error) {
        console.error('Fel vid radering av order:', error);
      }
    },

    // Markera order som klar
    async markOrderAsComplete(order) {
    try {
      await CompleteOrder({ orderId: order.orderId }); // Pass orderId in an object
      await this.fetchOrders();
    } catch (error) {
      console.error('Fel vid uppdatering av order:', error);
    }
  }
  },
  mounted() {
    this.fetchOrders(); // Hämtar ordrar
    this.intervalId = setInterval(this.fetchOrders, 5000); // Uppdatera ordrarna var 5:e sekund
  },
  beforeDestroy() {
    clearInterval(this.intervalId);
  }
};
</script>
