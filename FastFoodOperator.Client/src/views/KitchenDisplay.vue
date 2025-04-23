<template>
  <div class="kitchen-container">
    <h1>Köksdisplay</h1>

    <!--<button @click="toggleCompletedOrders">
      {{ showCompleted ? 'Dölj färdiga ordrar' : 'Visa färdiga ordrar' }}
    </button>
    -->


    <div class="orders-wrapper">
      <!-- Ej påbörjade ordrar -->
      <div class="orders-section">
        <h2>Ej Påbörjade Ordrar</h2>
        <div v-if="pendingOrders.length === 0">Inga påbörjade ordrar</div>
        <div v-for="order in pendingOrders" :key="order.orderId" :class="['order-card', getOrderCardClass(order)]">
          <p><strong>Order #: </strong>{{ order.orderNumber }}</p>
          <p v-if="order.customerNote"><strong>Notering:</strong> {{ order.customerNote }}</p>
          <p><strong>Skapad:</strong> {{ order.createdAt }}</p>

          <!-- Visa produkter -->
          <div>
            <strong>Produkter:</strong>
            <ul>
              <li v-for="product in order.orderProducts" :key="product.productId">
                {{ product.productName }} x{{ product.quantity }}
                <ul v-if="product.ingredients.length">
                  <li v-for="ingredient in product.ingredients" :key="ingredient.ingredientName">
                    - {{ ingredient.ingredientName }}
                  </li>
                </ul>
              </li>
            </ul>
          </div>

          <!-- Visa combos -->
          <div v-if="order.orderCombos.length">
            <strong>Combos:</strong>
            <ul>
              <li v-for="combo in order.orderCombos" :key="combo.comboId">
                {{ combo.comboName }} x{{ combo.quantity }}
                <ul v-if="combo.ingredients.length">
                  <li v-for="ingredient in combo.ingredients" :key="ingredient.ingredientName">
                    - {{ ingredient.ingredientName }}
                  </li>
                </ul>
              </li>
            </ul>
          </div>

          <!-- Markera som påbörjad -->
          <button @click="markOrderAsInProgress(order)">Markera som påbörjad</button>
        </div>
      </div>

      <!-- Pågående ordrar -->
      <div class="orders-section">
        <h2>Pågående Ordrar</h2>
        <div v-if="activeOrders.length === 0">Inga pågående ordrar</div>
        <div v-for="order in activeOrders" :key="order.orderId" :class="['order-card', getOrderCardClass(order)]">
          <p><strong>Order #: </strong>{{ order.orderNumber }}</p>
          <p v-if="order.customerNote"><strong>Notering:</strong> {{ order.customerNote }}</p>
          <p><strong>Skapad:</strong> {{ order.createdAt }}</p>

          <!-- Visa produkter -->
          <div>
            <strong>Produkter:</strong>
            <ul>
              <li v-for="product in order.orderProducts" :key="product.productId">
                {{ product.productName }} x{{ product.quantity }}
                <ul v-if="product.ingredients.length">
                  <li v-for="ingredient in product.ingredients" :key="ingredient.ingredientName">
                    - {{ ingredient.ingredientName }}
                  </li>
                </ul>
              </li>
            </ul>
          </div>

          <!-- Visa combos -->
          <div v-if="order.orderCombos.length">
            <strong>Combos:</strong>
            <ul>
              <li v-for="combo in order.orderCombos" :key="combo.comboId">
                {{ combo.comboName }} x{{ combo.quantity }}
                <ul v-if="combo.ingredients.length">
                  <li v-for="ingredient in combo.ingredients" :key="ingredient.ingredientName">
                    - {{ ingredient.ingredientName }}
                  </li>
                </ul>
              </li>
            </ul>
          </div>

          <!-- Markera som klar -->
          <button @click="markOrderAsComplete(order)">Markera som klar</button>
        </div>
      </div>

      <!-- Färdiga ordrar -->
      <div class="orders-section ready">
        <h2>Färdiga Ordrar</h2>
        <div v-if="completedOrders.length === 0">Inga färdiga ordrar</div>
        <div v-for="order in completedOrders" :key="order.orderId" class="order-card completed">
          <p><strong>Order #: </strong>{{ order.orderNumber }}</p>
          <p v-if="order.customerNote"><strong>Notering:</strong> {{ order.customerNote }}</p>
          <p><strong>Skapad:</strong> {{ order.createdAt }}</p>

          <!-- Visa produkter -->
          <div>
            <strong>Produkter:</strong>
            <ul>
              <li v-for="product in order.orderProducts" :key="product.productId">
                {{ product.productName }} x{{ product.quantity }}
                <ul v-if="product.ingredients.length">
                  <li v-for="ingredient in product.ingredients" :key="ingredient.ingredientName">
                    - {{ ingredient.ingredientName }}
                  </li>
                </ul>
              </li>
            </ul>
          </div>

          <!-- Visa combos -->
          <div v-if="order.orderCombos.length">
            <strong>Combos:</strong>
            <ul>
              <li v-for="combo in order.orderCombos" :key="combo.comboId">
                {{ combo.comboName }} x{{ combo.quantity }}
                <ul v-if="combo.ingredients.length">
                  <li v-for="ingredient in combo.ingredients" :key="ingredient.ingredientName">
                    - {{ ingredient.ingredientName }}
                  </li>
                </ul>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import '@/assets/kitchenDisplay.css';

export default {
  name: 'KitchenDisplay',
  data() {
    return {
      orders: [],
      intervalId: null,
      showCompleted: true, // ny flagga
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
        ? this.orders.filter(order => order.orderStatus === 2)
        : [];
    }
  },
  methods: {
    toggleCompletedOrders() {
      this.showCompleted = !this.showCompleted;
    },

    clearDisplay() {
      this.orders = [];
    },

    getOrderCardClass(order) {
      switch (order.orderStatus) {
        case 0:
          return 'pending';     // Ej påbörjad = röd
        case 1:
          return 'in-progress'; // Påbörjad = gul
        case 2:
          return 'completed';   // Färdig = grön
        default:
          return '';
      }
    },

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
            orderStatus: 1 // Sätt status till 1 påbbörjdd
          }),
        });

        if (!response.ok) throw new Error('Misslyckades att markera order som påbörjad');

        await this.fetchOrders(); // Uppdatera listan efter att ha markerat som påbörjad
      } catch (error) {
        console.error('Fel vid uppdatering av order:', error);
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
