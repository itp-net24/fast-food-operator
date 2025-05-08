<template>
  <div class="wrapper">
    <div class="receipt-box">
      <p class="thank-you">Thanks for your order!<br>Your order number:</p>
      <div class="order-number">{{ order.orderNumber }}</div>

      <hr class="dotted" />

      <h2 class="logo">ClaesBurgir</h2>
      <div class="info-line">
        <span>{{ order.createdAt }}</span>
        <span>K-{{ order.orderNumber.toString().padStart(6, '0') }}</span>
      </div>

      <hr class="dotted" />

      <table class="items">
        <thead>
        <tr>
          <th>Item</th>
          <th>Price</th>
          <th>Qty</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(item, index) in allItems" :key="index">
          <td>{{ item.name }}</td>
          <td>{{ formatPrice(item.price) }}</td>
          <td>{{ item.quantity }}</td>
        </tr>
        </tbody>
      </table>

      <hr class="dotted" />

      <div class="total-line">
        <strong>Total</strong>
        <strong>{{ formatPrice(order.orderFinalPrice) }} Kr</strong>
      </div>

      <div class="vat-line">
        <span>Included tax</span>
        <span>{{ formatPrice(order.totalTax) }} Kr</span>
      </div>

      <div class="vat-breakdown">
        <div class="vat-item" v-if="order.totalVatSixPercent > 0">
          <span>Tax 6%</span>
          <span>{{ formatPrice(order.totalVatSixPercent) }} Kr</span>
        </div>
        <div class="vat-item" v-if="order.totalVatTwelvePercent > 0">
          <span>Tax 12%</span>
          <span>{{ formatPrice(order.totalVatTwelvePercent) }} Kr</span>
        </div>
        <div class="vat-item" v-if="order.totalVatTwentyFivePercent > 0">
          <span>Tax 25%</span>
          <span>{{ formatPrice(order.totalVatTwentyFivePercent) }} Kr</span>
        </div>
      </div>

      <div class="footer">
        <span>Meow Meow</span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ReceiptComponent',
  props: {
    order: {
      type: Object,
      required: true
    }
  },
  computed: {
    allItems() {
      const products = this.order.products?.map(p => ({
        name: p.productName,
        price: p.productPrice,
        quantity: p.quantity
      })) || [];

      const combos = this.order.combos?.map(c => ({
        name: c.comboName,
        price: c.comboPrice,
        quantity: c.quantity
      })) || [];

      return [...products, ...combos];
    }
  },
  methods: {
    formatPrice(value) {
      return Number(value).toFixed(2).replace('.', ',');
    }
  }
};
</script>

<style scoped>
.wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
  font-family: 'Courier New', monospace;
}

.receipt-box {
  width: 300px;
  border: 1px solid #000;
  padding: 16px;
  text-align: center;
  background: #fff;
}

.thank-you {
  font-size: 14px;
}

.order-number {
  font-size: 48px;
  font-weight: bold;
  margin: 8px 0;
}

.dotted {
  border: none;
  border-top: 2px dotted #000;
  margin: 10px 0;
}

.logo {
  font-weight: bold;
  font-size: 20px;
  margin: 4px 0;
}

.info-line {
  display: flex;
  justify-content: space-between;
  font-size: 12px;
  margin-top: 4px;
}

.items {
  width: 100%;
  font-size: 14px;
  text-align: left;
  margin-top: 8px;
}
.items th,
.items td {
  padding: 2px 4px;
}
.items th {
  text-align: left;
  border-bottom: 1px solid #ddd;
}

.total-line,
.vat-line {
  display: flex;
  justify-content: space-between;
  font-weight: bold;
  font-size: 16px;
  margin-top: 10px;
}

.vat-line {
  font-weight: normal;
  font-size: 14px;
  margin-top: 4px;
}

.vat-breakdown {
  margin-top: 4px;
  font-size: 13px;
}
.vat-item {
  display: flex;
  justify-content: space-between;
}

.footer {
  margin-top: 16px;
  font-size: 12px;
}

.new-order-btn {
  margin-top: 16px;
  padding: 6px 12px;
  font-size: 14px;
  cursor: pointer;
}
</style>
