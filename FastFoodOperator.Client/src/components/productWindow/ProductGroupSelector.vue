<script setup lang="ts">
  import type {ComboProduct, ComboGroup} from "@/models/types.ts";

  const props = defineProps<Props>();

  interface Props {
    selectedProductIds: Record<number, number>;
    groups: ComboGroup[];
  }

  const emits = defineEmits<{
    (e: 'update-selection', groupId: number, productId: number): void
  }>();

  const updateSelection = (groupId: number, productId: number): void => {
    emits('update-selection', groupId, productId);
  }

  const getSelectedProduct = (group: ComboGroup): ComboProduct | null => {
    return group.comboProducts.find(cp => cp.id === props.selectedProductIds[group.id]) ?? null;
  }
</script>

<template>
  <ul class="combo-group-list">
    <li
        class="combo-group-item"
        v-for="(group, index) in groups"
        :key="index"
    >

      <div class="combo-select-wrapper">
        <select
            class="combo-select"
            v-model="selectedProductIds[group.id]"
        >
          <option
              v-for="comboProduct in group.comboProducts"
              :key="comboProduct.id"
              :value="comboProduct.id"
              @change="updateSelection(group.id, comboProduct.product.id)"
          >
            {{ comboProduct.product.name }}
          </option>
        </select>

        <slot name="group-selected-product" :comboProduct="getSelectedProduct(group)" />

      </div>
    </li>
  </ul>
</template>

<style scoped>
.combo-select {
  width: 100%;
  padding: 0.5rem 0.75rem;
  font-size: 1rem;
  border: 1px solid #ccc;
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

.combo-select:focus {
  border-color: #666;
  outline: none;
}

.combo-select-wrapper {
  position: relative;
}

ul {
  padding: 0;
  list-style: none;
}


hr {
  opacity: 20;
  margin-bottom: 1rem;
}

input[type="checkbox"] {
  width: 1rem;
  height: 1rem;
}
</style>