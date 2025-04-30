<script setup lang="ts">
  import { ref } from "vue"
  import ValueSelector from "@/components/ValueSelector.vue";

  const quantity = ref(1);

  const emits = defineEmits<{
    (e: 'confirm', quantity: number): number;
  }>();

  enum EventTypes {
    confirm = 'confirm'
  }
</script>

<template>
  <div class="cart-controls">
    <ValueSelector v-model:current-value="quantity" :minimum-value="1" :maximum-value="99" :step="1" />
    <button class="cart-button" @click="emits(EventTypes.confirm, quantity)">Add To Cart</button>
  </div>
</template>

<style scoped>
  .cart-button {
    width: 100%;
    height: 70%;
    background-color: orangered;
    color: white;
    border-radius: 0.7rem;
    border: none;
  }

  .cart-controls {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    height: 5rem;
    width: 100%;
    border-top: 1px solid black;
    background-color: white;
    border-radius: 0 0 1rem 1rem;
  }

  .cart-controls > * {
    margin: 1rem;
  }
</style>