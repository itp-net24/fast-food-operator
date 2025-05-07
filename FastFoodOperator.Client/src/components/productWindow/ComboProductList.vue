<script setup lang="ts">
  import type {ComboProduct} from "@/models/types.ts"

  const props = defineProps<Props>();

  interface Props {
    comboProducts: ComboProduct[];
  }
</script>

<template>
  <ul class="combo-product-container">
    <li
        v-for="(comboProduct, index) in comboProducts"
        :key="index"
        class="">
      {{comboProduct.product.name}}

      <slot name="combo-product" :comboProduct="comboProduct" />
    </li>
  </ul>
</template>

<style scoped>
  .combo-product-container {
    list-style-type: none;
    padding: 0;

    font-size: 1.1rem;
    font-weight: 600;
  }
</style>