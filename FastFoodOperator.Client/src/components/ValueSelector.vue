<script setup lang="ts">
  import { ref, watch } from "vue"
  import { clamp } from '@/utils/helpers.ts'

  const props = defineProps<Props>();

  interface Props {
    value: number;
    min: number;
    max: number;
    step: number;
  }

  const inputValue = ref<number>(props.value);

  watch(() => props.value, (newVal) => inputValue.value = newVal);

  const emits = defineEmits<{ (e: 'update:value', value: number): void; }>();

  const updateValue = (val: number) => emits('update:value', clamp(val, props.min, props.max));
</script>

<template>
  <div class="value-selector-container">
    <button class="value-selector-button" @click="updateValue(inputValue - step)" :disabled="value <= min">-</button>

    <input
      id="current-value"
      type="number"
      :min="min"
      :max="max"
      :step="step"
      v-model.number="inputValue"
      @change="updateValue(inputValue)"
    />

    <button class="value-selector-button" @click="updateValue(inputValue + step)" :disabled="value >= max">+</button>
  </div>
</template>

<style scoped>
  input[type=number]::-webkit-outer-spin-button,
  input[type=number]::-webkit-inner-spin-button {
    -webkit-appearance: none;
    margin: 0;
  }

  #current-value {
    width: 2rem;
    height: 2rem;

    font-size: 1.4rem;
    color: black;
    width: 2rem;
    text-align: center;
  }

  .value-selector-container {
    width: 7rem;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;

    user-select: none;
  }

  .value-selector-button {
    background-color: #f8f8f8;
    border-radius: 50%;
    aspect-ratio: 1;
    height: 1.8rem;

    border: 0.1rem solid rgba(0, 0, 0, 0.5);
    color: black;
    text-align: center;
  }
</style>
