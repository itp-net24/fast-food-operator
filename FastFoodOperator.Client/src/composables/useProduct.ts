import { computed, ref } from 'vue'
import type {
  CartContainer, CartItem,
  Combo,
  ComboGroup,
  ComboProduct, Ingredient,
  Product, Variant
} from '@/models/types.ts'
import {
  getProductTotalPrice,
  defaultProductOfCombo, defaultProductOfGroup
} from '@/utils/helpers.ts'
import { mapComboProductToCart, mapProductToCart, mapToCartContainer } from '@/utils/mappers.ts'

export default () => {
  const main = ref<CartItem>(null!);
  const $combo = ref<CartContainer | null>(null!);
  const $selectGroupProducts = ref<Record<number, ComboProduct>>({});

  const initializeProduct = (product: Product) => {
    resetState();

    const item = mapProductToCart(product);
    $combo.value = mapToCartContainer(product, [item]);

    main.value = item;
  }

  const initializeCombo = (combo: Combo) => {
    resetState();

    $combo.value = mapToCartContainer(combo, []);
    const mainProduct = defaultProductOfCombo(combo);
    if (!mainProduct) {
      console.log(`ERROR: no main product in combo: ${combo.name}`);
      return;
    }

    initializeComboProducts(combo.comboProducts, mainProduct);
    initializeGroupProducts(combo.comboGroups, mainProduct);
  }

  const initializeComboProducts = (products: ComboProduct[], mainProduct: ComboProduct | null) => {
    products.forEach(product => {
      const item = mapComboProductToCart(product, product && product == mainProduct);

      combo.value.products.push(item);

      // Important for main to reference the same object as in product array for ingredients to update correctly!
      if (item.__uid == mainProduct?.__uid)
        main.value = item;
    })
  }

  const initializeGroupProducts = (groups: ComboGroup[], mainProduct: ComboProduct | null) => {
    groups.forEach((cg: ComboGroup) => {
      const defaultComboProduct = defaultProductOfGroup(cg);
      if (!defaultComboProduct) {
        console.log(`ERROR: missing products in group: ${cg.name}`);
        return;
      }

      $selectGroupProducts.value[cg.id] = defaultComboProduct;
      combo.value.products.push(mapComboProductToCart(defaultComboProduct, defaultComboProduct == mainProduct));
    });
  }

  const combo = computed(() => {
    return $combo.value!;
  })

  const selectedProductFromGroup = (group: ComboGroup) => {
    return computed<ComboProduct>({
      get: (): ComboProduct => {
        return $selectGroupProducts.value[group.id];
      },

      set: (val: ComboProduct) => {
        const previousVal = selectedProductFromGroup(group).value;
        const index = combo.value.products.findIndex(v => v.__uid === previousVal.__uid);

        combo.value.products[index] = mapComboProductToCart(val, false);
        $selectGroupProducts.value[group.id] = val
      },
    });
  }

  const selectedVariantFromProduct = (product: ComboProduct) => {
    const selectedProduct = combo.value.products.find(p => p.__uid === product.__uid);

    return computed<Variant | null>({
      get: (): Variant | null => {
        return selectedProduct?.variant ?? null;
      },

      set: (val: Variant | null) => {
        if (!selectedProduct) return;
        selectedProduct.variant = val;
      }
    });
  }

  const selectedIngredients = computed<Ingredient[]>(() => main.value?.ingredients ?? []);

  const updateIngredients = (ingredient: Ingredient) => {
    if (!main.value.ingredients) return;

    const index = main.value.ingredients.findIndex(i => i.id === ingredient.id);

    if (index === -1)
      main.value.ingredients.push(ingredient);
    else
      main.value.ingredients.splice(index, 1);
  }

  const getTotal = computed(() => getProductTotalPrice(combo.value));

  const resetState = () => {
    main.value = null!;
    $combo.value = null;
    $selectGroupProducts.value = {};
  }

  return {
    combo: $combo,
    mainProduct: main,
    selectGroupProducts:$selectGroupProducts,
    initializeProduct,
    initializeCombo,
    selectedProductFromGroup,
    selectedVariantFromProduct,
    selectedIngredients,
    updateIngredients,
    getTotal,
  }
}
