<script setup lang="ts">
  import type {ComboProduct, ComboGroup} from "@/models/types.ts";

  const props = defineProps<Props>();

  interface Props {
    selection: ComboProduct;
    group: ComboGroup;
  }

  const emit = defineEmits<{
    (e: "update:selection", value: ComboProduct): void;
  }>();

  const onChange = (event: Event) => {
    const target: HTMLInputElement = event.target as HTMLInputElement;
    const id: number = Number(target.value);

    if (isNaN(id)) return;

    emit("update:selection", props.group.comboProducts.find(g => g.id === id) ?? props.selection);
  }
</script>

<template>
  <select
    class="group-select"
    :key="group.id"
    :value="selection.id"
    @change="onChange"
  >
    <option
      v-for="comboProduct in group.comboProducts"
      :key="comboProduct.id"
      :value="comboProduct.id"
    >
      {{ comboProduct.product.name }}
    </option>
  </select>
</template>

<style scoped>
.group-select {
  width: 100%;
  padding: 0.5rem 0.75rem;
  font-size: 1rem;
  border: 2px solid rgba(232, 232, 232, 0.53);
  border-radius: 6px;
  background-color: #fff;
  color: #333;
  appearance: none;
  background-image: url('data:image/svg+xml;charset=UTF-8,<svg fill="%23333" viewBox="0 0 24 24" width="24" height="24" xmlns="http://www.w3.org/2000/svg"><path d="M7 10l5 5 5-5z"/></svg>');
  background-repeat: no-repeat;
  background-position: right 0.5rem center;
  background-size: 1rem;
  cursor: pointer;
}

.group-select:focus {
  border-color: orangered;
}

.group-select:hover {
  border-color: orangered;
}
</style>
