<script setup lang="ts">
import {ref} from 'vue'
import {Product} from '@/models/product'
import {useCartStore} from '../stores/cart'
import {storeToRefs} from 'pinia'

const props = defineProps({
    product:Product
})

const cartStore = useCartStore()
const {cart} = storeToRefs(cartStore)

onMounted(() => {
    cartStore.loadCartInstance()
})

// function addToCart(){
//     cartStore.addToCart({id:props.product.id})
//     console.log("cart", cart)
// }


</script>

<template>
    <article>
        <div id="product-image" :style="{backgroundImage: `url(${product?.pictureUrl})`}">

        </div>
        <button @click="addToCart">Add to Cart</button>

        <h2> {{ product?.name }}</h2>
    </article>
</template>

<style scoped>

</style>