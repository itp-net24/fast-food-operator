<script setup lang="ts">
  import { ref, onMounted } from 'vue'
  import { GetIngredientsAsync } from "@/services/fetcher.ts"
  import type {Ingredient} from "@/models/types.ts";
  import { roundToPrecision } from '@/utils/helpers.ts'
  import { CURRENCY_SYMBOL, PRECISION_DISPLAY } from '../../../config.ts'

  const props = defineProps<Props>();

  interface Props {
    selectedIngredients: Ingredient[];
  }

  const emits = defineEmits<{
    (e: 'update-ingredients', ingredient: Ingredient): void;
  }>();

  const ingredients = ref<Ingredient[]>([]);
  const includedIngredients: number[] = props.selectedIngredients.map(i => i.id);

  const isSelected = (ingredient: Ingredient): boolean =>
    includedIngredients.some(i => i === ingredient.id)

  const getPrice = (ingredient: Ingredient): number => {
    if (isSelected(ingredient))
      return 0;
    else
      return ingredient.priceModifier;
  }

  const updateIngredients = (ingredient: Ingredient) => {
    console.log(props.selectedIngredients);
    emits('update-ingredients', { ...ingredient, priceModifier: getPrice(ingredient) });
  }

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
            @change="updateIngredients(ingredient)"
        />
        <span class="ingredient-name">{{ ingredient.name }}</span>
        <span class="ingredient-price">{{ getPrice(ingredient) == 0
          ? "Included"
          : "+" + roundToPrecision(getPrice(ingredient), PRECISION_DISPLAY) + CURRENCY_SYMBOL }}
        </span>
      </label>
    </li>
  </ul>
</template>

<style scoped>
.ingredient-list {
  user-select: none;
}

.ingredient-item {
  margin-bottom: 0.2rem;
  border-radius: 0.3rem;
  border: 1px solid transparent;
  padding: 0.3rem;
}

.ingredient-item:hover {
  background-color: #e7e7e7;
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

.ingredient-price {
  color: gray;
}

input[type=checkbox] {
  border: 1px solid black;
  padding: 0.5em;
  border-radius: 0.2rem;
  -webkit-appearance: none;

  cursor: pointer;
}

input[type=checkbox]:checked {
  background-color: var(--color-dark);
  background-image: url("data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCAyNCAyNCIgZmlsbD0ibm9uZSIgc3Ryb2tlPSJ3aGl0ZSIgc3Ryb2tlLXdpZHRoPSIzIiBzdHJva2UtbGluZWNhcD0icm91bmQiIHN0cm9rZS1saW5lam9pbj0icm91bmQiPjxwb2x5bGluZSBwb2ludHM9IjIwIDYgOSAxNyA0IDEyIi8+PC9zdmc+");
  background-repeat: no-repeat;
  background-position: center;
  background-size: 1em 1em;
}

input[type=checkbox]:focus {
  outline-color: transparent;
}
</style>
