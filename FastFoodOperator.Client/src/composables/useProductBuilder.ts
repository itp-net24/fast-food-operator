import { computed, ref } from 'vue'
import type {
  CartContainer,
  Combo,
  ComboGroup,
  ComboProduct, Ingredient,
  Product, Variant
} from '@/models/types.ts'
import { mapComboProductToCart } from '@/utils/cartMapper.ts'

export default () => {
  const $mainProduct = ref<Product>(null!);
  const $combo = ref<CartContainer | null>(null!);
  const $selectGroupProducts = ref<Record<number, ComboProduct>>({});

  const initializeProduct = (product: Product) => {
    $mainProduct.value = product;
  }

  const initializeCombo = (combo: Combo) => {
    $mainProduct.value = combo.mainComboProduct?.product ?? combo.comboProducts[0].product;
    $combo.value = {
      id: combo.id,
      type: "combo",
      name: combo.name,
      price: combo.basePrice,
      quantity: 1,
      products: [],
    };

    initializeComboProducts(combo.comboProducts);
    initializeGroupProducts(combo.comboGroups);
  }

  const initializeComboProducts = (products: ComboProduct[]) => {
    products.forEach(product => {
      combo.value.products.push(mapComboProductToCart(product, product.defaultProductVariant ?? product.product.variants[0], $mainProduct.value));
    })
  }
  const initializeGroupProducts = (groups: ComboGroup[]) => {
    groups.forEach((cg: ComboGroup) => {
      const defaultComboProduct = cg.defaultComboProduct ?? cg.comboProducts[0];

      if (!defaultComboProduct) {
        console.log(`ERROR: missing products in group: ${cg.name}`);
        return;
      }

      $selectGroupProducts.value[cg.id] = defaultComboProduct;

      const defaultProductVariant = defaultComboProduct.defaultProductVariant ?? defaultComboProduct.product.variants[0];
      combo.value.products.push(mapComboProductToCart(defaultComboProduct, defaultProductVariant, $mainProduct.value));
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

        const defaultProductVariant = val.defaultProductVariant ?? val.product.variants[0];
        combo.value.products[index] = mapComboProductToCart(val, defaultProductVariant, $mainProduct.value);
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

  const selectedIngredients = computed<Ingredient[]>(() => $mainProduct.value.ingredients);

  const updateIngredients = (ingredient: Ingredient) => {
    const index = $mainProduct.value.ingredients.findIndex(i => i.id === ingredient.id);
    if (index < 0)
      $mainProduct.value.ingredients.push(ingredient);
    else
      $mainProduct.value.ingredients.splice(index, 1);
  }

  return {
    combo: $combo,
    mainProduct: $mainProduct,
    selectGroupProducts:$selectGroupProducts,
    initializeProduct,
    initializeCombo,
    selectedProductFromGroup,
    selectedVariantFromProduct,
    selectedIngredients,
    updateIngredients,
  }
}
