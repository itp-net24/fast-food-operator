<template>
  <div class="kitchen-container">
    <h1>Kitchen Display</h1>

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
      return this.orders.filter(order => order.orderStatus === 0); // Ej påbörjade ordrar
    },
    activeOrders() {
      return this.orders.filter(order => order.orderStatus === 1); // Pågående ordrar
    },
    completedOrders() {
      return this.showCompleted
        ? this.orders.filter(order => order.orderStatus === 2) //Färdiga ordrar
        : [];
    }
  },
  methods: {



    // Hämtar ordrar från API
    async fetchOrders() {
      try {
        const res = await fetch('https://localhost:8080/api/order/GetOrders');
        const data = await res.json();
        this.orders = data;
      } catch (error) {
        console.error('Fel vid hämtning av ordrar:', error);
      }
    },

    // Markera order som påbörjad
    async markOrderAsInProgress(order) {
      try {
        const response = await fetch('https://localhost:8080/api/order/startOrder', {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            orderId: order.orderId,
            orderNumber: order.orderNumber,
            orderStatus: 1 // Sätt status till 1 påbörjad
          }),
        });

        if (!response.ok) throw new Error('Misslyckades att markera order som påbörjad');

        await this.fetchOrders(); // Uppdatera listan efter att ha markerat som påbörjad
      } catch (error) {
        console.error('Fel vid uppdatering av order:', error);
      }
    },
    //Radera Order

    async deleteOrder(order) {
      if (!confirm(`Are you sure you want to delete #${order.orderNumber}?`)) return;

      try {
        const response = await fetch(`https://localhost:8080/api/order/${order.orderId}`, {
          method: 'DELETE'
        });

        if (!response.ok) throw new Error('Kunde inte radera ordern');

        // Uppdatera listan efter borttagning
        await this.fetchOrders();

      } catch (error) {
        console.error('Fel vid radering av order:', error);
      }
    },

    // Markera order som klar
    async markOrderAsComplete(order) {
      try {
        const response = await fetch('https://localhost:8080/api/order/completeOrder', {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ orderId: order.orderId }),
        });

        if (!response.ok) throw new Error('Misslyckades att markera order som klar');

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
