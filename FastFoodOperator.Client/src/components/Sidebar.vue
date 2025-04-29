<script setup lang="ts">
<<<<<<< HEAD
import {ref, onMounted} from 'vue'
import {Category} from '@/models/category.ts'
import Fetcher from "@/ApiFetcher.ts"

const fetcher:Fetcher = new Fetcher();
const categories = ref<Category[] | null>();

const sideMenuOpen = ref<boolean>(false);

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

=======
>>>>>>> develop

</script>

<template>
<<<<<<< HEAD
    <div class="sidebar-container" v-bind:class="{'open border-sidebar': sideMenuOpen}">
        <button class="sidebar-toggle button-menu border-menu" v-on:click="sideMenuOpen = !sideMenuOpen">
            {{ sideMenuOpen ? '◂' : '▸' }}
        </button>
        <h2 class="title">Our Menu</h2>
        <ul class="category-list">
            <li class="category border-menu" v-for="category in categories" :key="category.id" v-on:click="CategoryClicked(category.id)">
                {{ category.name }}
            </li>
        </ul>
    </div>
</template>

<style scoped>
.slide-button {
    transition: transform 1s ease;
}

.sidebar-container {
    display: flex;
    flex-direction: column;

    padding: 0.5rem;
}

.sidebar-toggle {
    display: none;
    transition: transform 1s ease;
    font-size: 40px;
    padding: 0rem 0.6rem;
    margin: 0;
    text-align: center;
}

.title {
    white-space: nowrap;
}

.category-list {
    display: flex;
    flex-direction: column;
    gap: 0.25rem;
}

.category {
    font-size: 17px;
    padding: 1rem;
    cursor: pointer;
}

@media (max-width: 640px)
{
    .sidebar-toggle {
        display:block;
        position: absolute;
        right:-60px;
    }

    .sidebar-container {
        position:fixed;
        left: 0;
        background-color: white;
        padding: 1rem;
        margin-left: 2px;

        transform: translateX(-170px); /* mostly hidden, leave button peeking out */
        transition: transform 0.3s ease;
        z-index: 1000;
    }

    .sidebar-container.open {
        transform: translateX(0);
    }

    .border-sidebar {
        border: 3px solid black;
        border-radius: 4px;
        box-shadow: var(--box-shadow-default);
        transition: border-color 0.3s ease;
    }
}
</style>
=======

</template>
>>>>>>> develop
