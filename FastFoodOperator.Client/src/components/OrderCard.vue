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
          <ul class="ul-reset" v-if="product.ingredients">
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
          {{ combo.products }}
        </li>
      </ul>
    </div>

    <!-- Action-knapp för att markera som påbörjad eller klar -->
    <button
      class="kitchen-action-btn"
      v-if="status === 0"
      @click="async() => composable.setInProgressAsync(order)"
    >Mark as started</button>

    <button
      class="kitchen-action-btn"
      v-else-if="status === 1"
      @click="async() => composable.setCompleteAsync(order)"
    >Mark as finished</button>

    <!-- Raderaknapp -->
    <button class="kitchen-action-btn" @click="async() => composable.deleteOrderAsync(order)">
      Delete order
    </button>

  </div>
</template>

<script setup lang="ts">

import type { OrdersComposable } from '@/composables/useOrders.ts'

const props = defineProps<Props>();

interface Props {
  composable: OrdersComposable;
  order: object;
  status: number;
}

const statusClass = () => {
  switch (props.status) {
    case 0:
      return 'pending';    // Ej påbörjad
    case 1:
      return 'in-progress'; // Pågående
    case 2:
      return 'completed';   // Färdig
    default:
      return '';
  }
}
</script>
