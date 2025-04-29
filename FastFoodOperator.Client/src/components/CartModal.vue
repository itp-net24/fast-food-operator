<script setup lang="ts">
    import Sidebar from './Sidebar.vue'
    import CartItem from './CartItem.vue'
    import {ref, onMounted} from 'vue'
    import {Product} from '@/models/product.ts'
    import Fetcher from "@/ApiFetcher.ts"
    import {useCartStore} from '../stores/cart'
    import {storeToRefs} from 'pinia'
    
    
    const fetcher = new Fetcher();
    

    onMounted(async () => {
  try {
    cartStore.loadCartInstance()

  } catch (err) {
    console.error('error:', err);
  }
})

const cartStore = useCartStore()
const {cart} = storeToRefs(cartStore)

function checkOut(){
  cartStore.checkOut()
}






var modal = document.getElementById("cartModal")!;
var cartBtn = document.getElementById("cartButton")!;
var span = document.getElementsByClassName("close")[0] as HTMLElement;

console.log(cartBtn);

cartBtn.onclick = function(){
  console.log(modal)
modal.style.display = "block";
}

const openCart = () =>{

modal.style.display = "none";
}

window.onclick = function(event){
  if(event.target == modal){
    modal.style.display = "none";
  }
}

</script>

<template>
    <div class="menu-container">
        
            <!-- <aside>
                <Sidebar />
            </aside> -->

            <main>
              
              <button id="cartButton" @click="openCart"> Cart </button>
              <div id="cartModal" class="modal">
                <div class="modal-content">
                  <span class="close">&times;</span>
             
                </div>
              </div>

                <div v-for="(cartProduct,index) in cart.cartProducts" :key="index" class="articles-container">
                    <CartItem :cartProduct="cartProduct" />
                </div>
            </main>
        
    </div>

    <div>
      {{ cart }}
      <button @click="checkOut">Check out cart</button>
      <!-- <button @click="clearCart">Clear Cart</button> -->

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