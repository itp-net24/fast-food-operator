<script setup lang="ts">
import {onMounted, ref} from "vue";
import type {
  Combo,
  Product,
  CartItem,
  CartContainer,
  Ingredient,
  BaseProduct
} from '@/models/types.ts'
import {ProductType} from "@/enums/enums.ts"
import {GetComboAsync, GetProductAsync} from "@/services/productService.ts";

import PopupModal from "@/components/PopupModal.vue";
import ProductCartControls from "@/components/productWindow/ProductCartControls.vue";
import IngredientsList from "@/components/productWindow/IngredientsList.vue";
import BaseProductDetails from "@/components/productWindow/BaseProductDetails.vue";
import VariantSelector from "@/components/productWindow/VariantSelector.vue";
import ComboProductList from "@/components/productWindow/ComboProductList.vue";
import ProductGroupSelector from "@/components/productWindow/ProductGroupSelector.vue";

import {
  mapProductToCart,
  mapComboProductToCart,
} from '@/utils/cartMapper.ts'
import { getComboPrice, getProductPrice } from '@/utils/productHelpers.ts'

import useProductBuilder from "@/composables/useProductBuilder.ts"
const builder = useProductBuilder();

const props = defineProps<Props>();

interface Props {
  id: number;
  type: ProductType;
}

const product = ref<Product | Combo | null>(null);

const getMainProduct = (): Product | null => {
  if(props.type === ProductType.product) {
    const p = product.value as Product;
    return p ?? null;
  }
  else if (props.type === ProductType.combo) {
    const c = product.value as Combo;
    return c.mainComboProduct?.product ?? c.comboProducts[0].product;
  }
  else {
    console.log("Could not determine product type!");
    return null;
  }
}

// Ingredient Selection
const updateSelectedIngredientIds = (ingredient: Ingredient) => {
  if (props.type === ProductType.product) {
    const p = product.value as Product;

    const exists = p.ingredients.some(i => i.id == ingredient.id);
    if (exists) {
      p.ingredients = p.ingredients.filter(i => i.id != ingredient.id);
    } else {
      p.ingredients.push(ingredient);
    }
  }
  else if (props.type === ProductType.combo) {
    const c = product.value as Combo;
    const p: Product = c.mainComboProduct?.product ?? c.comboProducts[0].product;

    const exists: boolean = p.ingredients.some(i => i.id == ingredient.id);
    if (exists) {
      p.ingredients = p.ingredients.filter(i => i.id != ingredient.id);
    } else {
      p.ingredients.push(ingredient);
    }
  }
  else {
    console.log("Could not determine product type!")
  }
}


// Cart Controls
const handleConfirm = (quantity: number) => {
  console.log(builder.combo.value);

  // const item = buildCartItem(quantity);
  // console.log(item);
}

const buildCartItem = (quantity: number): CartContainer | null => {
  const products: CartItem[] = [];

  if (props.type === ProductType.product) {
    const p = product.value as Product;
    products.push(mapProductToCart(p));
  }
  else if (props.type === ProductType.combo) {
    const c = product.value as Combo;
    products.push(
      ...c.comboProducts.map(cp => mapComboProductToCart(cp, getSelectedVariant(cp), getMainProduct())),

      ...c.comboGroups.flatMap(group => {
        const cp = builder.selectedProductFromGroup(group).value;
        return cp ? mapComboProductToCart(cp, getSelectedVariant(cp), getMainProduct()) : []
      }));
  }
  else {
    console.log("Could not determine product type!");
    return null;
  }

  const mainProduct = getMainProduct();
  if (!mainProduct) return null;

  const item: CartContainer = {
    id: mainProduct.id,
    type: props.type,
    name: mainProduct.name,
    price: totalPrice.value!,
    quantity: quantity,
    products: products,
  }

  return item;
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

        <IngredientsList
          v-if="product"
          v-model:selected-ingredients="getMainProduct().ingredients"
          @update-ingredients="updateSelectedIngredientIds"
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
