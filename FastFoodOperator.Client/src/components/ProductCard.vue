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

// function addToCart(){
//     cartStore.addToCart({
//         id: props.product?.id,
//         name: props.product?.name,
//         description: props.product?.description,
//         basePrice: props.product?.basePrice,
//         pictureUrl: props.product?.pictureUrl })
//         console.log("cart", cart)
// }


</script>

<template>
    <article class="border-menu">
        <div id="product-image" :style="{ backgroundImage: 'url(' + product.pictureUrl + ')' }">

        </div>
        <h2> {{ product?.name }}</h2>
        
        <button @click="">Add to Cart</button>
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

    background-color: white;
}

h2 {
    padding: var(--spacing-xs);
    min-height: 60px;
}

#product-image {
    height: 256px;
    width: 256px;
    background-repeat: no-repeat;
    background-size: cover;
}
</style>
