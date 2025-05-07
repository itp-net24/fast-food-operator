<script setup lang="ts">
  import RadioSelector from '@/components/RadioSelector.vue'
  import type { Variant } from '@/models/types.ts'
  import { getVariantDiscount } from '@/utils/helpers.ts'
  import { CURRENCY_SYMBOL, PRECISION_DISPLAY, PRECISION_INTERNAL } from '../../../config.ts'

  const props = defineProps<Props>();

  interface Props {
    selection: Variant | null;
    variants: Variant[];
    defaultVariant?: Variant | null;
    uid?: number;
  }

  const emits = defineEmits<{ (e: 'update:selection', variant: Variant): void }>();

  const defaultVariant = props.defaultVariant ?? props.variants[0];

  const displayPriceMessage = (id: number): string => {
    const variant = props.variants.find(v => v.id === id);
    const price = getVariantDiscount(variant, defaultVariant, PRECISION_DISPLAY);

    return price > 0 ? `+${price}${CURRENCY_SYMBOL}` : 'Included'
  }

  const onVariantSelected = (variant: Variant): void => {
    const v = { ...variant, priceModifier: getVariantDiscount(variant, defaultVariant, PRECISION_INTERNAL) }
    emits('update:selection', v);
  }
</script>

<template v-if="selection">
  <RadioSelector
    :selection="selection"
    :options="variants"
    :option-key="(option) => `${uid}-${option.id}`"
    @update:selection="(variant) => onVariantSelected(variant)"
  >
    <template #label="{ option }">
      <div class="variant-details">
        <p class="variant-name">{{ option.name }}</p>
        <p class="variant-price">{{ displayPriceMessage(option.id) }}</p>
      </div>
    </template>
  </RadioSelector>
</template>

<style scoped>
  .variant-details {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;

    width: 100%;
    padding: 0.4rem;
  }

  .variant-details > p {
    margin: 0;
  }

  .variant-price {
    color: gray;
  }
</style>
