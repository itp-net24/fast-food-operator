<script setup lang="ts">
  import { ref, onMounted, reactive } from "vue"
  import { GetIngredientsAsync } from "@/services/productService.ts"
  import type {Ingredient} from "@/models/types.ts";

  const props = defineProps<Props>();

  interface Props {
    selectedIngredients: Ingredient[];
  }

  const emits = defineEmits<{
    (e: 'update-ingredients', ingredient: Ingredient): void;
  }>();

  const includedIngredients: number[] = props.selectedIngredients.map(i => i.id);

  const isSelected = (ingredient: Ingredient): boolean => {
    return props.selectedIngredients.some(i => i.id === ingredient.id);
  }

  const getPrice = (ingredient: Ingredient): number => {
    if (includedIngredients.some(i => i === ingredient.id)) {
      return 0;
    }
    else return ingredient.priceModifier;
  }

  const handleUpdate = (ingredient: Ingredient) => {
    emits('update-ingredients', reactive(ingredient));
  }

  const ingredients = ref<Ingredient[]>([]);

  onMounted(async () => {
    ingredients.value = await GetIngredientsAsync();
  })
</script>

<template>
  <ul class="ingredient-list">
    <li v-for="ingredient in ingredients"
        :key="ingredient.id"
        class="ingredient-item"
    >
      <label class="ingredient-content">
        <input
            type="checkbox"
            :checked="isSelected(ingredient)"
            @change="handleUpdate(ingredient)"
        />
        <span class="ingredient-name">{{ ingredient.name }}</span>
        <span class="ingredient-price">{{ getPrice(ingredient) == 0 ? "Included" : "+" + getPrice(ingredient) + "kr" }}</span>
      </label>
    </li>
  </ul>
</template>

<style scoped>
hr {
  opacity: 20;
  margin-bottom: 1rem;
}

.ingredient-list {
  padding: 0;
  list-style: none;

  user-select: none;
}

.ingredient-item {
  margin-bottom: 0.2rem;
  border-radius: 0.3rem;
  border: 1px solid transparent;
}

.ingredient-item:hover {
  background-color: lightpink;
  border: 1px solid rgba(0, 0, 0, 0.3);
}

.ingredient-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  cursor: pointer;
  padding: 0.4rem;
}

input[type="checkbox"] {
  width: 1rem;
  height: 1rem;
}

.ingredient-name {
  flex-grow: 1;
  margin-left: 0.8rem;
}
</style>