<script setup lang="ts">
import { ref, onMounted, reactive, watch } from 'vue'
  import { GetIngredientsAsync } from "@/services/productService.ts"
  import type {Ingredient} from "@/models/types.ts";
import { getVariantDiscount, roundToPrecision } from '@/utils/helpers.ts'
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
        <span class="ingredient-price">{{ getPrice(ingredient) == 0 ? "Included" : "+" + roundToPrecision(getPrice(ingredient), PRECISION_DISPLAY) + CURRENCY_SYMBOL
          }}</span>
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
