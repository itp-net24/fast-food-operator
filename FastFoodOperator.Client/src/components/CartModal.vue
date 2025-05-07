<script setup lang="ts">
import {ref} from 'vue'
import {useCart} from '@/stores/testCart.ts'
import {storeToRefs} from 'pinia'

const cartStore = useCart();
const {cart} = storeToRefs(cartStore)

function checkOut(){
  cartStore.checkout(textBox.value)
  textBox.value = '';
}

function clearCart(){
  cartStore.clearCart()
}

const textBox = ref('')
</script>

<template>
  <div class="menu-container">

    <!-- <aside>
        <Sidebar />
    </aside> -->

    <main>

      <!--              <button class="button-basic" id="cartButton" @click="openCart">Open Cart </button>-->
      <div id="cartModal" class="modal">
        <div class="modal-content">
          <span class="close">&times;</span>

        </div>
      </div>

      <div v-for="(cartProduct,index) in cart" :key="index" class="menu-container">
        <!--                    <CartItem :cartProduct="cartProduct" />-->
      </div>
      <div class="menu-container">
        <!--                 Total price without tax: {{ CartTotal }}-->
      </div>
      <div class="menu-container">
        <!--                  Total price with tax: {{ TaxTotal }}-->
      </div>
    </main>

  </div>

  <div>
    {{ cart }}

    <textarea v-model="textBox" placeholder="leave a comment with your order here"></textarea>

    <button class="button-basic" @click="checkOut"> Checkout </button>

    <button class="button-basic" @click="clearCart">Clear Cart</button>

  </div>
</template>

<style scoped>
.menu-container {
  padding-top: 1rem;
}

main {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
}

.modal{
  display: none;
  position: fixed;
  z-index: 1;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  background-color: rgb(0,0,0);
  background-color: rgba(0,0,0,0.4);
}

.modal-content {
  background-color: #fefefe;
  margin: 15% auto;
  padding: 20px;
  border: 1px solid #888;
  width: 80%;
}

.close {
  color: #aaa;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: black;
  text-decoration: none;
  cursor: pointer;
}

</style>
