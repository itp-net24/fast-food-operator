<template>
  <div>
    <ReceiptComponent :order="mockOrder" @new-order="handleNewOrder" />
  </div>
</template>

<script>
import ReceiptComponent from '../components/ReceiptComponent.vue';

export default {
  components: {
    ReceiptComponent
  },
  data() {
    return {
      mockOrder: {
        products: [
          { productName: 'Beer', productPrice: 40, quantity: 1 }
        ],
        combos: [
          { comboName: 'Cheeseburger Combo', comboPrice: 25.99, quantity: 1 }
        ],
        createdAt: '2025-05-03 23:48',
        orderNumber: 1027,
        totalVatSixPercent: 0,
        totalVatTwelvePercent: 2.78,
        totalVatTwentyFivePercent: 8,
        totalTax: 10.78,
        orderFinalPrice: 65.99
      }
    };
  },
  methods: {

  }
};
</script>
