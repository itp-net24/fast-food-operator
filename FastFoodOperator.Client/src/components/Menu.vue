<script setup lang="ts">
    import Sidebar from './Sidebar.vue'
    import ProductCard from './ProductCard.vue'
    import {ref, onMounted} from 'vue'
    import {Product} from '@/models/product.ts'
    import Fetcher from "@/ApiFetcher.ts"
    import {useCartStore} from '../stores/cart'
    import {storeToRefs} from 'pinia'
    import CartModal from './CartModal.vue'
    
    
    const fetcher = new Fetcher();
    const products = ref<Product[] | null>([]);

    onMounted(async () => {
  try {
    products.value = await fetcher.getProducts(32, 0);
    console.log(products.value);
    cartStore.loadCartInstance();
  } catch (err) {
    console.error('error:', err);
  }
})

async function OnCategoryClicked(categoryId: number) {
  try {

    const result = await fetcher.getProductsByCategoryId(categoryId, 100, 0);
    if (result != null)
    {
      products.value = result;
    }
  } catch (err) {
    console.error('error:', err);
  }
}

const cartStore = useCartStore()
const {cart} = storeToRefs(cartStore)

function checkOut(){
  cartStore.checkOut()
        console.log('running button for checkout')

}






// var modal = document.getElementById("cartModal")!;
// var cartBtn = document.getElementById("cartButton")!;
// var span = document.getElementsByClassName("close")[0] as HTMLElement;

// console.log(cartBtn);

// cartBtn.onclick = function(){
//   console.log(modal)
// modal.style.display = "block";
// }

// const openCart = () =>{

// modal.style.display = "none";
// }

// window.onclick = function(event){
//   if(event.target == modal){
//     modal.style.display = "none";
//   }
// }

</script>

<template>
    <div class="company-title">
      <img src="@/assets/Claes_Burgir1.png" alt="Företagslogotyp" class="company-logo">
    </div>

    <div class="menu-container">
        
            <aside>
                <Sidebar v-on:category-clicked="OnCategoryClicked"/>
            </aside>

            <main>
              
              <!-- <button id="cartButton" @click="openCart"> Cart </button>
              <div id="cartModal" class="modal">
                <div class="modal-content">
                  <span class="close">&times;</span>
             
                </div>
              </div> -->

                <div v-for="product in products" :key="product.id" class="articles-container">
                    <ProductCard :product="product" />
                </div>
            </main>
        
    </div>

    <div>

    </div>
    <CartModal/>
</template>

<style scoped>
.menu-container {
  padding-top: 1rem;
}

.menu-container {
  display: flex;
}

main {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
}

@media (max-width: 640px)
{
  main {
    justify-content: center;
  }
}

/* .modal{
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
} */

</style>