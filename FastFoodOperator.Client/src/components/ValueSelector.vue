<script setup lang="ts">
  const props = defineProps<Props>();

  interface Props {
    currentValue: number;
    minimumValue: number;
    maximumValue: number;
    step: number;
  }

  const emits = defineEmits<{
    (e: 'update:currentValue', value: number): void;
  }>();

  const updateValue = (val: number) => {
    if (val >= props.minimumValue && val <= props.maximumValue) {
      emits('update:currentValue', val);
    }
  };
</script>

<template>
  <div class="wrapper">
    <button class="button-basic button" id="button-decrement" @click="updateValue(currentValue - step)" :disabled="currentValue <= minimumValue"></button>
    <span id="value">{{currentValue}}</span>
    <button class="button-basic button" id="button-increment" @click="updateValue(currentValue + step)" :disabled="currentValue >= maximumValue"></button>
  </div>
</template>

<style scoped>
  #value {
    font-size: 1.4rem;
    color: black;
    width: 2rem;
    text-align: center;
  }

  .wrapper {
    width: 7rem;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;

    user-select: none;
  }

  #button-decrement::before {
    content: "-";
  }

  #button-increment::before {
    content: "+";
  }

  .button {
    background-color: #f8f8f8;
    border-radius: 50%;
    aspect-ratio: 1;
    height: 1.8rem;

    border: 0.1rem solid rgba(0, 0, 0, 0.5);
    color: black;
    text-align: center;
  }
</style>
