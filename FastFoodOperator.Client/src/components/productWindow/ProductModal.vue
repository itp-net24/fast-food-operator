<script setup lang="ts">
import {onMounted, ref} from "vue";
import type {
  Combo,
  Product,
  BaseProduct
} from '@/models/types.ts'
import {ProductType} from "@/enums/enums.ts"

import PopupModal from "@/components/PopupModal.vue";
import ProductCartControls from "@/components/productWindow/ProductCartControls.vue";
import IngredientSelector from "@/components/productWindow/IngredientSelector.vue";
import BaseProductDetails from "@/components/productWindow/BaseProductDetails.vue";
import VariantSelector from "@/components/productWindow/VariantSelector.vue";
import ComboProductList from "@/components/productWindow/ComboProductList.vue";
import ProductGroupSelector from "@/components/productWindow/ProductGroupSelector.vue";

import {GetComboAsync, GetProductAsync} from "@/services/productService.ts";
import useProductBuilder from "@/composables/useProductBuilder.ts"

const builder = useProductBuilder();

const props = defineProps<Props>();

interface Props {
  id: number;
  type: ProductType;
}

const product = ref<Product | Combo | null>(null);


const handleConfirm = () => {
  console.log(builder.combo.value);
}


// Popup
const popup = ref<boolean>(true);


onMounted(async () => {
  if (props.type === ProductType.product) {
    const p = await GetProductAsync(props.id);
    product.value = p as Product;
  }
  else if (props.type === ProductType.combo) {
    const c = await GetComboAsync(props.id);
    product.value = c as Combo;
    builder.initializeCombo(c);
  }
  else {
    console.log("Could not determine product type!");
  }
});



// const totalPrice = computed(() => {
//   if (props.type === ProductType.product) {
//     const p = product.value as Product;
//     return getProductPrice(p);
//   }
//   else if (props.type === ProductType.combo) {
//     const c = product.value as Combo;
//     return getComboPrice(c, (cg: ComboGroup) => builder.selectedProductFromGroup(cg).value, (cp: ComboProduct | null) => builder.selectedVariantFromProduct(cp).value);
//   }
//   else {
//     return null;
//   }
// });
</script>

<template>
  <PopupModal
    v-if="popup && product"
    :enable-close-button="true"
    :enable-blur="true"
    :close-on-outside-click="true"
    @close="popup=false">

    <div class="container">
      <div class="wrapper">
        <BaseProductDetails
          :base-product="product as BaseProduct"
          :total-price="0"
        />

        <!-- Single Product -->
        <ComboProductList
          v-if="type === ProductType.combo"
          :combo-products="product.comboProducts">

          <template #combo-product="{comboProduct}">
            <VariantSelector
              v-if="builder.selectedVariantFromProduct(comboProduct).value"
              :key="comboProduct.id"
              v-model:selection="builder.selectedVariantFromProduct(comboProduct).value!"
              :combo-product="comboProduct"
            />
          </template>
        </ComboProductList>

        <!-- Group Products -->
        <div
          v-for="group in product.comboGroups"
          :key="group.id">

          <ProductGroupSelector
            v-model:selection="builder.selectedProductFromGroup(group).value"
            :group="group"
          />

          <VariantSelector
            v-if="builder.selectedVariantFromProduct(builder.selectedProductFromGroup(group).value).value"
            v-model:selection="builder.selectedVariantFromProduct(builder.selectedProductFromGroup(group).value).value!"
            :combo-product="builder.selectedProductFromGroup(group).value" />
        </div>

        <hr />
        <h2>Ingredients</h2>

        <IngredientSelector
          v-model:selected-ingredients="builder.selectedIngredients.value"
          @update-ingredients="builder.updateIngredients"
        />
      </div>

      <ProductCartControls
        class="controls"
        @confirm="handleConfirm"
      />
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

.controls{
  position: absolute;
  bottom: 0;
  left: 0;
}
</style>
