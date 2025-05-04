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
    selectedId.value = update.id;
  })

  watch(selectedId, (id) => {
    const option = props.options.find(o => o.id === id);
    if (!option) return;

    emits('update:selection', option);
  });
</script>

<template>
  <ul v-if="selectedId">
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
    border-color: orangered;
  }

  input[type=radio] {
    border: 1px solid black;
    padding: 0.5em;
    border-radius: 0.2rem;
    -webkit-appearance: none;

    cursor: pointer;
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
