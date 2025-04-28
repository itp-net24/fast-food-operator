<script setup lang="ts">
import {onMounted, ref} from 'vue'
import {Product} from '@/models/product'
import {useCartStore} from '../stores/cart'
import {storeToRefs} from 'pinia'
import type {cartProduct} from '@/models/interfaces'

interface Props {
    product:Product
}
const props = defineProps<Props>()

const cartStore = useCartStore()
const {cart} = storeToRefs(cartStore)

onMounted(() => {
    cartStore.loadCartInstance()
})

function addToCart(){
    cartStore.addToCart(props.product)
        console.log("cart", cart)
}


</script>

<template>
    <article>
        <div id="product-image" :style="{ backgroundImage: 'url(' + (product && product.pictureUrl ? product.pictureUrl : '') + ')' }">

        </div>
        <h2> {{ product?.name }}</h2>
        
        <button @click="addToCart">Add to Cart</button>
    </article>
</template>

<style scoped>
* {
    margin: 0;
    margin-top: 0;
}
article {
    width: 270px;
    height: 100%;
    display:flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
    gap: 4px;

    border: 2px solid rgba(182, 181, 181, 0.613);
    border-radius: 4px;

    transition: border-color 0.3s ease;
}

article:hover {
    border-color: gray;
}

h2 {
    padding: var(--spacing-xs);
    min-height: 60px;
}

button {
  padding: 0.6rem 1.2rem;
  margin-bottom: var(--spacing-xs);
  background-color: var(--color-primary);
  color: white;
  font-weight: 550;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-size: 1rem;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

button:hover {
  background-color: var(--color-hover);
  transform: scale(1.05);
}

#product-image {
    height: 256px;
    width: 256px;
    background-repeat: no-repeat;
    background-size: cover;
}
</style>
