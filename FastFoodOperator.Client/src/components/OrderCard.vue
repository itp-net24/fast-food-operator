<template>
  <div :class="['order-card', statusClass]">
    <p>
      <strong>Order #: </strong>{{ order.orderNumber }}
      <span
        v-if="order.customerNote && order.customerNote !== 'string' && order.customerNote.trim() !== ''"
        class="note-icon"
        title="Finns en notering">
        ⚠️VARNING SE NOTERING⚠️
      </span>
    </p>
    <p v-if="order.customerNote && order.customerNote !== 'string' && order.customerNote.trim() !== ''">
      <strong>Notering:</strong> {{ order.customerNote }}
    </p>
    <p><strong>Skapad:</strong> {{ order.createdAt }}</p>

    <!-- Produkter -->
    <div>
      <strong>Produkter:</strong>
      <ul>
        <li v-for="(product, index) in order.orderProducts" :key="index">
          {{ product.productName }} x{{ product.quantity }}
          <ul v-if="product.ingredients.length">
            <li v-for="(ingredient, index) in product.ingredients" :key="ingredient.ingredientName">
              - {{ ingredient }}
            </li>
          </ul>
        </li>
      </ul>
    </div>

    <!-- Combos -->
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

    <!-- Action-knapp för att markera som påbörjad eller klar -->
    <button
      v-if="status === 0"
      @click="$emit('start', order)"
    >Markera som påbörjad</button>

    <button
      v-else-if="status === 1"
      @click="$emit('complete', order)"
    >Markera som klar</button>
  </div>
</template>

<script>
export default {
  props: {
    order: Object,
    status: Number,
  },
  computed: {
    statusClass() {
      switch (this.status) {
        case 0: return 'pending';    // Ej påbörjad
        case 1: return 'in-progress'; // Pågående
        case 2: return 'completed';   // Färdig
        default: return '';
      }
    }
  }
}
</script>

<style scoped>
.order-card {
  border: 1px solid #e0e0e0;
  padding: 1rem;
  margin: 0.75rem 0;
  border-radius: 10px;
  background-color: #fafafa;
  text-align: left;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.order-card:hover {
  background-color: #f1f1f1;
  transform: translateX(5px);
}

.order-card.pending {
  border-left: 6px solid #dc3545; /* Röd */
}

.order-card.in-progress {
  border-left: 6px solid #ffc107; /* Gul */
}

.order-card.completed {
  background-color: #e3f8e3;
  border-left: 6px solid #28a745; /* Grön */
  border-color: #a2d8a2;
}
.note-icon {
  margin-left: 6px;
  color: #ff9800;
  font-size: 1.2rem;
}
</style>
