import { computed, ref } from 'vue'
import type {
  Combo,
  ComboGroup,
  ComboProduct,
  Product,
} from '@/models/types.ts'

export default () => {
  const $mainProduct = ref<Product>(null!);
  const $combo = ref<Combo | null>(null);
  const $selectGroupProducts = ref<Record<number, ComboProduct>>({});

  const initializeProduct = (product: Product) => {
    $mainProduct.value = product;
  }

  const initializeCombo = (combo: Combo) => {
    $mainProduct.value = combo.mainComboProduct?.product ?? combo.comboProducts[0].product;
    $combo.value = combo;
    initializeGroupProducts(combo.comboGroups);
  }

  const initializeGroupProducts = (groups: ComboGroup[]) => {
    groups.forEach((cg: ComboGroup) => {
      const defaultComboProduct = cg.defaultComboProduct ?? cg.comboProducts[0];

      if (!defaultComboProduct) {
        console.log(`ERROR: missing products in group: ${cg.name}`);
        return;
      }

      $selectGroupProducts.value[cg.id] = defaultComboProduct;
    });
  }

  const selectedProductFromGroup = (group: ComboGroup) => {
    return computed<ComboProduct>({
      get: () => $selectGroupProducts.value[group.id],
      set: (val: ComboProduct) => $selectGroupProducts.value[group.id] = val,
    })
  }

  return {
    combo: $combo,
    mainProduct: $mainProduct,
    selectGroupProducts:$selectGroupProducts,
    initializeProduct,
    initializeCombo,
    selectedProductFromGroup,
  }
}
