<script setup lang="ts">
  import { ref, watch } from 'vue'
  import type { ComboProduct, Variant } from '@/models/types.ts'
  import { defaultVariantOfProduct, getVariantDiscount } from '@/utils/helpers.ts'
  import { CURRENCY_SYMBOL, PRECISION_DISPLAY } from '../../../config.ts'

  const props = defineProps<Props>();

  interface Props {
    selection: Variant;
    comboProduct: ComboProduct;
  }

  const emits = defineEmits<{ (e: 'update:selection', variant: Variant): void }>();

  const selectedId = ref<number>(defaultVariantOfProduct(props.comboProduct).id);

  const selectedVariant = ref<Variant>(props.comboProduct.product.variants.find(v => v.id === selectedId.value)!);

  watch (() => props.comboProduct, (newCombo) => {
    selectedId.value = defaultVariantOfProduct(newCombo).id;
  });

  watch(selectedId, (newId) => {
    const variant = props.comboProduct.product.variants.find(v => v.id === newId);
    if (!variant) return;

    const newVariant = { ...variant, priceModifier: getVariantDiscount(variant, selectedVariant.value) };
    emits('update:selection', newVariant);
  });

  const isVariantFree = (variant: Variant): boolean => getVariantDiscount(variant, selectedVariant.value) <= 0;
</script>

<template>
  <ul class="variant-list">
    <li
        v-for="variant in comboProduct.product.variants"
        :key="variant.id"
        class="variant-item"
    >
      <label class="variant-label variant-details">
        <div class="variant-details">
        <input
            type="radio"
            :name="comboProduct.product.name"
            v-model="selectedId"
            :value="variant.id"
            class="variant-radio"
        />
          <span class="variant-name">{{ variant.name }}</span>
          <span class="variant-price">
            {{ isVariantFree(variant) ? "Included" : "+" + getVariantDiscount(variant, selectedVariant, PRECISION_DISPLAY) + CURRENCY_SYMBOL }}
          </span>
        </div>
      </label>
    </li>
  </ul>
</template>

<style scoped>
  .variant-details {
    display: flex;
    flex-direction: row;
    justify-content: start;
    align-items: center;
  }

  .variant-name{
    flex-grow: 1;
    margin-left: 0.6rem;
  }

  .variant-price {
    color: gray;
  }

  .variant-list {
    list-style: none;
    padding: 0.5rem;

    font-size: 0.9rem;
    font-weight: 100;
  }

  .variant-label {
    display: block;
    padding: 0.4rem;
  }

  input[type=radio] {
    border: 1px solid black;
    padding: 0.5em;
    border-radius: 0.2rem;
    -webkit-appearance: none;
  }

  input[type=radio]:checked {
    background-color: cornflowerblue;
    background-image: url("data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCAyNCAyNCIgZmlsbD0ibm9uZSIgc3Ryb2tlPSJ3aGl0ZSIgc3Ryb2tlLXdpZHRoPSIzIiBzdHJva2UtbGluZWNhcD0icm91bmQiIHN0cm9rZS1saW5lam9pbj0icm91bmQiPjxwb2x5bGluZSBwb2ludHM9IjIwIDYgOSAxNyA0IDEyIi8+PC9zdmc+");
    background-repeat: no-repeat;
    background-position: center;
    background-size: 1em 1em;
  }

  input[type=radio]:focus {
    outline-color: transparent;
  }
</style>
