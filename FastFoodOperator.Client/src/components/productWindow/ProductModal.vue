<script setup lang="ts">
import {onMounted, ref, computed} from "vue";
import type {
  Combo,
  ComboGroup,
  ComboProduct,
  Product,
  ProductToCart,
  ProductVariant,
  ComboToCart,
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

const props = defineProps<Props>();

  interface Props {
    id: number;
    type: ProductType;
  }

  const product = ref<Product | Combo | null>(null);


  // Group Product Selection
  const selectedProductIds = ref<Record<number, number>>({});

  const initializeSelectedProductIds = (combo: Combo) => {
    combo.comboGroups.forEach(g => {
      selectedProductIds.value[g.id] = g.defaultComboProductId ?? 0;
    })
  }

  const handleComboGroupSelection = (groupId: number, productId: number): void => {
    selectedProductIds[groupId] = productId;
  }




  // Variant Selection
  const selectedVariantIds = ref<Record<number, number>>({});

  const getSelectedProduct = (group: ComboGroup): ComboProduct | null => {
    return group.comboProducts.find(cp => cp.id === selectedProductIds.value[group.id]) ?? null;
  }

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



const initializeSelectedVariantIds = (product: Product | Combo) => {
    if (props.type === ProductType.product) {
      if (product.variants.length <= 0) return;
      selectedVariantIds.value[product.id] = product.variants[0];
    }
    else if (props.type === ProductType.combo) {
      product.comboProducts.forEach(cp => {
        selectedVariantIds.value[cp.product.id] = cp.defaultProductVariantId;
      })

      product.comboGroups.forEach(gp => {
        gp.comboProducts.forEach(cp => {
          selectedVariantIds.value[cp.product.id] = cp.defaultProductVariantId;
        })
      })
    }
    else {
      console.log("Could not determine product type!")
    }

    console.log(selectedVariantIds.value);
  }

const getSelectedVariant = (cp: ComboProduct): ProductVariant | null => {
  const selectVariantId: number = selectedVariantIds.value[cp.product.id];
  return cp.product.variants.find(v => v.id === selectVariantId) ?? null;
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
  const handleConfirm = () => {
    console.log("Variant Selections", selectedVariantIds.value);
    console.log("Group Selections", selectedProductIds.value);

    const combo = product.value as Combo;
    console.log(buildComboToCart(combo));
  }



  const buildComboToCart = (combo: Combo): ComboToCart => {
    const products: ProductToCart[] = [
      ...combo.comboProducts.map(cp => mapComboProductToCart(cp, getSelectedVariant(cp), getMainProduct())),

      ...combo.comboGroups.flatMap(group => {
        const cp = getSelectedProduct(group);
        return cp ? mapComboProductToCart(cp, getSelectedVariant(cp), getMainProduct()) : [];
      })
    ];

    const cart: ComboToCart = {
      id: combo.id,
      name: combo.name,
      price: totalPrice.value!,
      product: products,
    }

    return cart;
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
      initializeSelectedProductIds(product.value);
    }
    else {
      console.log("Could not determine product type!");
    }

    initializeSelectedVariantIds(product.value);
  });




  const totalPrice = computed(() => {
    if (props.type === ProductType.product) {
      const p = product.value as Product;
      return p.basePrice + p.variants[selectedVariantIds.value[p.id]].priceModifier;
    }
    else if (props.type === ProductType.combo) {
      const c = product.value as Combo;


      const comboTotal = c.comboProducts.reduce((acc, cp) => {
        const selectedId = selectedVariantIds.value[cp.product.id];
        const selectedModifier = cp.product.variants[selectedId]?.priceModifier ?? 0;
        const defaultModifier = cp.defaultProductVariant?.priceModifier ?? 0;

        return acc + selectedModifier - defaultModifier;
      }, 0);

      const groupTotal = c.comboGroups.reduce((acc, cg) => {
        const cp = getSelectedProduct(cg);
        // console.log("Selected CP:", cp);

        const selectVariantId = selectedVariantIds.value[cp.product.id];
        const selectVariant= cp.product.variants.find(v => v.id === selectVariantId);
        // console.log("Selected Variant:", selectVariant);

        const selectedModifier = selectVariant.priceModifier ?? 0;
        const defaultModifier = cp.defaultProductVariant?.priceModifier ?? 0;

        return acc + Math.max(selectedModifier - defaultModifier, 0);
      }, 0);

      const mp = c.mainComboProduct?.product ?? c.comboProducts[0].product
      const ingredientsTotal = mp.ingredients.reduce((acc, i) => {
        return acc + i.priceModifier;
      }, 0);

      return Math.round((c.basePrice + comboTotal + groupTotal + ingredientsTotal) * 100) / 100;
    }
  })
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
            :total-price="totalPrice"
        />

        <!-- Single Product -->
        <ComboProductList
            v-if="type === ProductType.combo"
            :combo-products="product.comboProducts">

          <template #combo-product="{comboProduct}">
            <VariantSelector
                :key="comboProduct.id"
                v-model:variant-id="selectedVariantIds[comboProduct.product.id]"
                :combo-product="comboProduct"
            />
          </template>
        </ComboProductList>

        <!-- Group Products -->
        <ProductGroupSelector
            v-if="type === ProductType.combo"
            :selected-product-ids="selectedProductIds"
            :groups="product.comboGroups"
            @update-selection="handleComboGroupSelection">

          <template #group-selected-product="{comboProduct}">
            <VariantSelector
                :key="comboProduct.id"
                v-model:variant-id="selectedVariantIds[comboProduct.product.id]"
                :combo-product="comboProduct"
            />
          </template>
        </ProductGroupSelector>

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
