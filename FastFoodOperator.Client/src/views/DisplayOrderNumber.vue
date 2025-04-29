<template>
  <div class="display-container">
    <div class="company-title">
      <img src="@/assets/Claes_Burgir1.png" alt="Företagslogotyp" class="company-logo">
    </div>

    <div class="display-box">
      <h2>Tillagas</h2>
      <div class="number-display">
        <p v-if="activeOrderNumbers.length > 0" v-for="(num, index) in activeOrderNumbers" :key="index">
          #{{ num }}
        </p>
        <p v-else>Inga aktiva ordrar</p>
      </div>
    </div>

    <div class="completed-orders-box">
      <h2>Redo att hämtas</h2>
      <div class="number-display">
        <p
          v-for="(order, index) in completedOrderNumbers"
          :key="'ready-' + index"
          class="completed-highlight"
        >
          #{{ order.number }}
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import '@/assets/DisplayOrderNumber.css';

export default {
  name: 'DisplayOrder',
  data() {
    return {
      activeOrderNumbers: [],
      completedOrderNumbers: [],
      intervalId: null,
      cleanupIntervalId: null,
    };
  },
  mounted() {
    this.getOrderNumbers(); // Direkt
    this.intervalId = setInterval(this.getOrderNumbers, 5000); // Uppdatera data
    this.cleanupIntervalId = setInterval(this.cleanupOldOrders, 5000); // Rensa gamla entries, funkar ej
  },
  beforeUnmount() {
    clearInterval(this.intervalId);
    clearInterval(this.cleanupIntervalId);
  },
  methods: {
    async getOrderNumbers() {
      try {
        const response = await fetch('https://localhost:8080/api/order/GetOrders');

        if (!response.ok) throw new Error(`API-svar: ${response.statusText}`);

        const orders = await response.json();

        // Aktiva
        this.activeOrderNumbers = orders
          .filter(order => order.orderStatus === 0 || order.orderStatus === 1)
          .map(order => order.orderNumber);


        const existingNumbers = this.completedOrderNumbers.map(o => o.number);

        const newCompleted = orders
          .filter(order => order.orderStatus === 2) // Endast färdiga ordrar
          .map(order => order.orderNumber)
          .filter(num => !existingNumbers.includes(num))
          .map(num => ({
            number: num,
            timestamp: Date.now(),
          }));


        this.completedOrderNumbers.push(...newCompleted);

      } catch (error) {
        console.error('Kunde inte hämta könummer:', error);
      }
    },

    cleanupOldOrders() {
      const now = Date.now();
      const timeout = 60000; // 60 sekunder, funkar inte :(

      this.completedOrderNumbers = this.completedOrderNumbers.filter(order => {
        return now - order.timestamp < timeout;
      });
    }
  },
};
</script>
