<script setup lang="ts">
import {ref} from 'vue'
import {useCart} from '@/stores/testCart.ts'
import {storeToRefs} from 'pinia'
import PopupModal from '@/components/PopupModal.vue'
import CartItem from '@/components/CartItem.vue'

const cartStore = useCart();
const {cart} = storeToRefs(cartStore)

const props = defineProps<Props>();

interface Props {
  visible: boolean;
}

const emits = defineEmits<{
  (e: 'close'): void;
}>();

const mobileBreakpoint: number = 400;
const isMobile = ref<boolean>(false);

const updateIsMobile = () => {
  isMobile.value = window.matchMedia(`(max-width: ${mobileBreakpoint}px)`).matches;
}

const checkout = () => {
  cartStore.checkout(textBox.value)
  textBox.value = '';
}

const clearCart = () => {
  cartStore.clearCart()
}

const textBox = ref('')
</script>

<template>
  <PopupModal
    v-if="visible"
    :enable-close-button="isMobile"
    :enable-blur="true"
    :close-on-outside-click="true"
    @close="() => emits('close')">

    <div class="menu-container">
      <main>
        <div v-for="(item, index) in cart" :key="index" class="menu-container">
          <CartItem :product="item" />
        </div>
      </main>


    <div>
      <textarea v-model="textBox" placeholder="leave a comment with your order here"></textarea>

      <div>
        <button class="bajs" @click="checkout"> Checkout </button>
        <button class="bajs" @click="clearCart">Clear Cart</button>
      </div>

    </div>
    </div>
  </PopupModal>
</template>

<style scoped>
.menu-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  padding-top: 1rem;
  color: black;
  background-color: white;
  width: 30vw;

}

main {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
}

.bajs {
  background-color: lightgray;
  padding: 1rem;
  color: black;
  margin: 1rem;
  border-radius: 1rem;
}
</style>
