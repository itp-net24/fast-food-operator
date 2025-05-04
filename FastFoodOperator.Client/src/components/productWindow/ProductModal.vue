<script setup lang="ts">
import {onMounted, ref} from "vue";
import type {
  Combo,
  Product,
  BaseProduct
} from '@/models/types.ts'
import {ProductType} from "@/enums/enums.ts"

import PopupModal from "@/components/PopupModal.vue";
import IngredientSelector from "@/components/productWindow/IngredientSelector.vue";
import BaseProductDetails from "@/components/productWindow/BaseProductDetails.vue";
import ProductGroupSelector from "@/components/productWindow/ProductGroupSelector.vue";
import ValueSelector from '@/components/ValueSelector.vue'
import VariantSelector from '@/components/productWindow/VariantSelector.vue'

import { GetComboAsync, GetProductAsync } from '@/services/productService.ts'
import {  defaultVariantOfProduct } from '@/utils/helpers.ts'
import useProduct from "@/composables/useProduct.ts"

const builder = useProduct();

const props = defineProps<Props>();

interface Props {
  id: number;
  type: ProductType;
}

const product = ref<Product>(null!);
const combo = ref<Combo | null>(null);


const handleConfirm = () => {
  console.log(builder.combo.value);
}


// Popup
const popup = ref<boolean>(true);

onMounted(async () => {
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
});
</script>

<template>
  <PopupModal
    v-if="popup && (product || combo)"
    :enable-close-button="true"
    :enable-blur="true"
    :close-on-outside-click="true"
    @close="popup=false">

    <div class="container">
      <div class="wrapper">
        <BaseProductDetails v-if="product || combo"
          :base-product="product as BaseProduct ?? combo as BaseProduct"
          :total-price="builder.getTotal.value"
        />

        <div v-if="product">
          <VariantSelector
            v-model:selection="builder.mainProduct.value.variant"
            :variants="product.variants"
            :default-variant="product.defaultVariant"
          />
        </div>

        <!-- Single Product -->
        <div v-if="combo">
          <div v-for="comboProduct in combo.comboProducts" :key="comboProduct.__uid">
            {{ comboProduct.product.name }}

            <VariantSelector
              v-model:selection="builder.selectedVariantFromProduct(comboProduct).value"
              :variants="comboProduct.product.variants"
              :default-variant="defaultVariantOfProduct(comboProduct)"
              :uid="comboProduct.__uid"
            />
          </div>

          <!-- Group Products -->
          <div
            v-for="group in combo.comboGroups"
            :key="group.id">

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
        <div>
          <hr />

          <h2>Ingredients</h2>

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
  </PopupModal>
</template>

<style scoped>
.container {
  position: relative;
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  width: clamp(360px, 30vw, 500px);
  height: 80vh;

  border-radius: 1rem;

  color: black;
  background-color: white;

  overflow: hidden;
}

.wrapper {
  overflow-y: auto;
  margin-bottom: 5rem;
  padding: 2rem 2rem 0 2rem;
}

.wrapper::-webkit-scrollbar {
  width: .5rem; /* Width of the scrollbar */
}

.wrapper::-webkit-scrollbar-track {
  background: transparent; /* Make the track transparent */
}

.wrapper::-webkit-scrollbar-thumb {
  background: rgba(255, 69, 0, 0.75); /* Light grey color for the thumb */
  border-radius: 1rem; /* Rounded corners */
}

.wrapper::-webkit-scrollbar-thumb:hover {
  background: #ff4500; /* Darker grey on hover */
}

.cart-button {
  width: 100%;
  height: 70%;
  background-color: orangered;
  color: white;
  border-radius: 0.7rem;
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
  border-top: 1px solid black;
  background-color: white;
  border-radius: 0 0 1rem 1rem;
}

.cart-controls > * {
  margin: 2rem;
}
</style>
