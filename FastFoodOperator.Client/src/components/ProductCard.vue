<script setup lang="ts">
import {ref} from 'vue'
import {Product} from '@/models/product'

defineProps({
    product:Product
})
</script>

<template>
    <article>
        <div id="product-image" :style="{backgroundImage: `url(${product?.pictureUrl})`}">

        </div>

        <h2> {{ product?.name }}</h2>
    </article>
</template>

<style scoped>

</style>