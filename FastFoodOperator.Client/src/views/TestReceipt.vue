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
      mockOrder: window.history.state?.order || null
    };
  },
  methods: {

  }
};
</script>
