<script setup lang="ts">
import {ref, onMounted} from 'vue'
import {Category} from '@/models/category.ts'
import Fetcher from "@/ApiFetcher.ts"

const fetcher:Fetcher = new Fetcher();
const categories = ref<Category[] | null>([
    {id: 0, name: 'Hamburgare'},
    {id: 0, name: 'Kyckling & fisk'},
    {id: 0, name: 'Vegetariskt'},
    {id: 0, name: 'Sallad'},
    {id: 0, name: 'Tillbehör & snacks'},
    {id: 0, name: 'Desserter'},
]);

function CategoryClicked(category:Category):void {
    emit('categoryClicked', category);
}

// onMounted( async () => {
//     const result:Category[] | null = await fetcher.getCategories();

//     if (result == null)
//     {
//         categories.value = [];
//     } 
//     else 
//     {
//         categories.value = result;
//     }
// })

const emit = defineEmits<{
    (e: 'categoryClicked', data: Category):void
}>();


</script>

<template>
    <div class="sidebar-container">
        <h2 class="title">Categories</h2>
        <ul class="category-list">
            <li class="category" v-for="category in categories" :key="category.id" v-on:click="() => CategoryClicked">
                {{ category.name }}
            </li>
        </ul>
    </div>
</template>

<style>
.sidebar-container {
    display: flex;
    flex-direction: column;
}

/* .title {

} */

.category-list {
    display: flex;
    flex-direction: column;
}

.category {
    padding: 1rem;
}
</style>