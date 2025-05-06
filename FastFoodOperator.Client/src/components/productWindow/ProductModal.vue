<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
import type {
  Combo,
  Product,
} from '@/models/types.ts'
import {ProductType} from "@/enums/enums.ts"
import PopupModal from "@/components/PopupModal.vue";
import IngredientSelector from "@/components/productWindow/IngredientSelector.vue";
import ProductGroupSelector from "@/components/productWindow/ProductGroupSelector.vue";
import ValueSelector from '@/components/ValueSelector.vue'
import VariantSelector from '@/components/productWindow/VariantSelector.vue'

import { GetComboAsync, GetProductAsync } from '@/services/fetcher.ts'
import { defaultVariantOfProduct, roundToPrecision } from '@/utils/helpers.ts'
import useProduct from "@/composables/useProduct.ts"
import { CURRENCY_SYMBOL } from '../../../config.ts'

const builder = useProduct();

const props = defineProps<Props>();

interface Props {
  id: number;
  type: ProductType;
  visible: boolean;
}

const emits = defineEmits<{
  (e: 'close'): void;
}>();

const product = ref<Product>(null!);
const combo = ref<Combo | null>(null);


const handleConfirm = () => {
  console.log(builder.combo.value);
}

const mobileBreakpoint: number = 450;
const isMobile = ref<boolean>(false);

const updateIsMobile = () => {
  isMobile.value = window.matchMedia(`(max-width: ${mobileBreakpoint}px)`).matches;
}

onMounted(async () => {
  const mediaQuery = window.matchMedia(`(max-width: ${mobileBreakpoint}px)`);
  mediaQuery.addEventListener('change', updateIsMobile);

  await fetchComboOrProduct();
});

watch([() => props.id, () => props.type], async () => {
  await fetchComboOrProduct();
});

const fetchComboOrProduct = async () => {
  product.value = null!;
  combo.value = null;

  if (props.type === ProductType.product) {
    const p = await GetProductAsync(props.id);
    product.value = p;
    builder.initializeProduct(p);
  }
  else if (props.type === ProductType.combo) {
    const c = await GetComboAsync(props.id);
    combo.value = c;
    builder.initializeCombo(c);
  }
  else {
    console.log("Could not determine product type!");
  }
}
</script>

<template v-if="builder.mainProduct.value && visible">
  <PopupModal
    v-if="visible && (product || combo)"
    :enable-close-button="isMobile"
    :enable-blur="true"
    :close-on-outside-click="true"
    @close="() => emits('close')">

    <div class="container">

      <div class="container-scrollable">
        <img v-if="builder.combo.value?.imageUrl" class="product-image" :src="builder.combo.value?.imageUrl" :alt="`image of ${builder.combo.value.name}`" />
        <div class="wrapper">

          <div class="product-content">
            <h2 class="main-product-name">{{ builder.combo.value?.name}}</h2>
            <span class="product-price">{{ roundToPrecision(builder.getTotal.value, 0) + CURRENCY_SYMBOL }}</span>
          </div>

          <div v-if="product">
            <VariantSelector
              v-model:selection="builder.mainProduct.value.variant"
              :variants="product.variants"
              :default-variant="product.defaultVariant"
            />
          </div>

          <!-- Single Product -->
          <div v-if="combo">
            <div v-for="comboProduct in combo.comboProducts"
                 class="product-list"
                 :key="comboProduct.__uid">
              <h2 class="product-name">{{ comboProduct.product.name }}</h2>

              <VariantSelector
                v-model:selection="builder.selectedVariantFromProduct(comboProduct).value"
                :variants="comboProduct.product.variants"
                :default-variant="defaultVariantOfProduct(comboProduct)"
                :uid="comboProduct.__uid"
              />
            </div>

            <!-- Group Products -->
            <div v-for="group in combo.comboGroups"
                 class="product-list"
                 :key="group.id">
              <h2 class="product-name">{{ group.name }}</h2>

              <ProductGroupSelector
                v-model:selection="builder.selectedProductFromGroup(group).value"
                :group="group"
              />

              <VariantSelector
                v-model:selection="builder.selectedVariantFromProduct(builder.selectedProductFromGroup(group).value).value"
                :variants="builder.selectedProductFromGroup(group).value.product.variants"
                :default-variant="defaultVariantOfProduct(builder.selectedProductFromGroup(group).value)"
              />
            </div>
          </div>

          <!-- Ingredients -->
          <div class="product-list">
            <h2 class="product-name">Ingredients</h2>

            <IngredientSelector
              v-model:selected-ingredients="builder.selectedIngredients.value"
              @update-ingredients="builder.updateIngredients"
            />
          </div>
        </div>

        <div class="cart-controls">
          <ValueSelector v-model:value="builder.combo.value.quantity" :min="1" :max="99" :step="1" />
          <button class="cart-button" @click="handleConfirm">Add To Cart</button>
        </div>
      </div>

    </div>
  </PopupModal>
</template>

<style scoped>
.container {
  position: relative;
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  width: clamp(400px, 50vw, 600px);
  height: 80vh;

  color: var(--color-dark);
  background-color: var(--color-white);

  border-radius: var(--border-radius);
  box-shadow: var(--box-shadow-default);

  overflow: hidden;
}

@media screen and (max-width: 450px) {
  .container {
    width: 100vw;
    min-width: 400px;
    height: 100vh;
  }
}

.container-scrollable {
  overflow-y: auto;
  margin-bottom: 5rem;
}

.wrapper {
  padding: 1rem 1rem 1rem 1rem;
}

.container-scrollable::-webkit-scrollbar {
  width: 8px;
}

.container-scrollable::-webkit-scrollbar-track {
  background: transparent;
}

.container-scrollable::-webkit-scrollbar-thumb {
  background: rgba(255, 69, 0, 0.75);
  border-radius: var(--border-radius);
}

.container-scrollable::-webkit-scrollbar-thumb:hover {
  background: var(--color-secondary);
}

.product-image {
  width: 100%;
  aspect-ratio: 16/9;
  height: auto;
  object-fit: cover;
  object-position: bottom;
  display: block;
  padding: 0;
}

.product-content {
  display: flex;
  justify-content: space-between;
  align-items: start;

  padding: 1rem;
  margin-bottom: 1rem;
}

.product-name {
  font-size: 1.1rem;
  font-weight: 700;
  margin: 0.4rem;
}

.main-product-name {
  font-size: 1.5rem;
  font-weight: 800;
}

.product-price {
  font-size: 1.3rem;
  font-weight: 600;
}

.product-list {
  background-color: var(--color-background);
  border-radius: var(--border-radius);
  margin: 1rem 0;
  padding: 1rem;
}


.cart-button {
  width: 100%;
  height: 70%;
  background-color: orangered;
  color: var(--color-white);
  border-radius: var(--border-radius);
  border: none;
}

.cart-controls {
  position: absolute;
  bottom: 0;
  left: 0;

  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  height: 5rem;
  width: 100%;

  border-top: 1px solid var(--color-border);
  box-shadow: 0 -3px 10px rgba(0, 0, 0, 0.2);

  background-color: var(--color-white);
  border-radius: 0 0 var(--border-radius) var(--border-radius);
}

.cart-controls > * {
  margin: 1rem;
}
</style>
