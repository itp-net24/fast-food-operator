<script setup lang="ts">
import {ref, onMounted} from 'vue'
import {Category} from '@/models/category.ts'
import Fetcher from "@/ApiFetcher.ts"

const fetcher:Fetcher = new Fetcher();
const categories = ref<Category[] | null>();

function CategoryClicked(categoryId:number):void {
    emit('categoryClicked', categoryId);
}

onMounted( async () => {
    const result:Category[] | null = await fetcher.getCategories();

    if (result == null)
    {
        categories.value = [];
    } 
    else 
    {
        categories.value = result;
    }
})

const emit = defineEmits<{
    (e: 'categoryClicked', categoryId: number):void
}>();


</script>

<template>
    <div class="sidebar-container">
        <h2 class="title">Kategorier</h2>
        <ul class="category-list">
            <li class="category border-menu" v-for="category in categories" :key="category.id" v-on:click="CategoryClicked(category.id)">
                {{ category.name }}
            </li>
        </ul>
    </div>
</template>

<style scoped>
.sidebar-container {
    display: flex;
    flex-direction: column;

    padding: 0.5rem;
}

/* .title {

} */

.category-list {
    display: flex;
    flex-direction: column;
}

.category {
    font-size: 17px;
    padding: 1rem;
    cursor: pointer;
}
</style>