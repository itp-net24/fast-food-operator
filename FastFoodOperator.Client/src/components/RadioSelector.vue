<script setup lang="ts">
  import { ref, watch } from 'vue'

  const props = defineProps<Props>();

  interface Props<T extends Option = Option> {
    selection: T | null;
    options: T[];
    optionKey?: (T) => string;
  }

  const emits = defineEmits<{ (e: 'update:selection', out: T): void; }>();

  interface Option {
    id: number;
    name: string;
  }

  const selectedId = ref<number>(props.selection?.id ?? props.options[0]?.id ?? 0);

  watch(() => props.selection, (update) => {
    if (!update) return;
    selectedId.value = update.id;
  })

  watch(selectedId, (id) => {
    const option = props.options.find(o => o.id === id);
    if (!option) return;

    emits('update:selection', option);
  });
</script>

<template v-if="selectedId && options && options.length > 0">
  <ul class="radio-list">
    <li
      v-for="option in props.options"
      :key="optionKey(option)"
    >
      <label class="label-container">
        <input
          type="radio"
          :name="optionKey(option)"
          v-model="selectedId"
          :value="option.id"
        />
        <slot name="label" :option="option">
          {{ option.name }}
        </slot>
      </label>
    </li>
  </ul>
</template>

<style scoped>
  .radio-list {
    list-style: none;
    padding: 0;
  }

  .label-container {
    display: flex;
    align-items: center;
    justify-content: start;

    padding: 0.4rem;
    border: 2px solid transparent;
    border-radius: 0.5rem;

    cursor: pointer;
  }

  .label-container:hover {
    background-color: #e7e7e7;
  }

  input[type=radio] {
    border: 2px solid black;
    padding: 0.5em;
    border-radius: 50%;
    -webkit-appearance: none;

    cursor: pointer;
    scale: 1;

    transition: transform .3s ease, box-shadow .3s ease;
  }

  input[type=radio]:checked {
    box-shadow: inset 0 0 0 4px black;
    transform: scale(1.1);
    background-repeat: no-repeat;
    background-position: center;
    background-size: 1em 1em;
  }

  input[type=radio]:focus {
    outline-color: transparent;
  }
</style>
