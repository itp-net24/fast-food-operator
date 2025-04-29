<script setup lang="ts">
import {onMounted, ref} from 'vue'
import {Product} from '@/models/product'
import {useCartStore} from '../stores/cart'
import {storeToRefs} from 'pinia'

interface Props {
    product:Product
}
const props = defineProps<Props>()

const cartStore = useCartStore()
const {cart} = storeToRefs(cartStore)

onMounted(() => {
    cartStore.loadCartInstance()
})

<<<<<<< HEAD
// function addToCart(){
//     cartStore.addToCart({
//         id: props.product?.id,
//         name: props.product?.name,
//         description: props.product?.description,
//         basePrice: props.product?.basePrice,
//         pictureUrl: props.product?.pictureUrl })
//         console.log("cart", cart)
// }
=======
function addToCart(){
    cartStore.addToCart({
        id: props.product?.id,
        name: props.product?.name,
        description: props.product?.description,
        basePrice: props.product?.basePrice,
        pictureUrl: props.product?.pictureUrl })
        console.log("cart", cart)
}
>>>>>>> develop


</script>

<template>
<<<<<<< HEAD
    <article class="border-menu">
        <div id="product-image" :style="{ backgroundImage: 'url(' + product.pictureUrl + ')' }">
=======
    <article>
        <div id="product-image" :style="{ backgroundImage: 'url(' + (product && product.pictureUrl ? product.pictureUrl : '') + ')' }">
>>>>>>> develop

        </div>
        <h2> {{ product?.name }}</h2>
        
<<<<<<< HEAD
        <button class="button-menu" @click="">Add to Cart</button>
=======
        <button @click="addToCart">Add to Cart</button>
>>>>>>> develop
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

<<<<<<< HEAD
    background-color: white;
=======
    border: 2px solid rgba(182, 181, 181, 0.613);
    border-radius: 4px;

    transition: border-color 0.3s ease;
}

article:hover {
    border-color: gray;
>>>>>>> develop
}

h2 {
    padding: var(--spacing-xs);
    min-height: 60px;
}

<<<<<<< HEAD
=======
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

>>>>>>> develop
#product-image {
    height: 256px;
    width: 256px;
    background-repeat: no-repeat;
    background-size: cover;
}
</style>
