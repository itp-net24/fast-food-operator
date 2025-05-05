<!-- För Visning -->
<template>
  <div :class="['order-card', statusClass]">
    <p class="paragraph">
      <strong>Order #: </strong>{{ order.orderNumber }}
      <span
        v-if="order.customerNote && order.customerNote !== 'string' && order.customerNote.trim() !== ''"
        class="note-icon"
        title="Notation">
        ⚠️WARNING SEE BELOW⚠️
      </span>
    </p>
    <p class="paragraph" v-if="order.customerNote && order.customerNote !== 'string' && order.customerNote.trim() !== ''">
      <strong>Note:</strong> {{ order.customerNote }}
    </p>
    <p class="paragraph"><strong>Created:</strong> {{ order.createdAt }}</p>

    <!-- Produkter -->
    <div>
      <strong>Products:</strong>
      <ul class="ul-reset">
        <li v-for="(product, index) in order.orderProducts" :key="index">
          {{ product.productName }} x{{ product.quantity }}
          <ul class="ul-reset" v-if="product.ingredients.length">
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
      <ul class="ul-reset">
        <li v-for="combo in order.orderCombos" :key="combo.comboId">
          {{ combo.comboName }} x{{ combo.quantity }}
          <ul class="ul-reset" v-if="combo.ingredients.length">
            <li v-for="ingredient in combo.ingredients" :key="ingredient.ingredientName">
              - {{ ingredient.ingredientName }}
            </li>
          </ul>
        </li>
      </ul>
    </div>

    <!-- Action-knapp för att markera som påbörjad eller klar -->
    <button
      class="button-basic"
      v-if="status === 0"
      @click="$emit('start', order)"
    >Mark as started</button>

    <button
      class="button-basic"
      v-else-if="status === 1"
      @click="$emit('complete', order)"
    >Mark as finished</button>

    <!-- Raderaknapp -->
    <button class="button-basic" @click="$emit('delete', order)">
      Delete order
    </button>

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
      //bara för style
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
