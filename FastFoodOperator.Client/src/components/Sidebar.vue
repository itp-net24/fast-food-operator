<script setup lang="ts">
import {ref, onMounted} from 'vue'
import { getTagsAsync } from '@/services/fetcher.ts'
import type { Tag } from '@/models/types.ts'

const tags = ref<Tag[] | null>();

const sideMenuOpen = ref<boolean>(false);
const activeCategory = ref<number | null>(null);

function handleTagClicked(tagId: number): void {
    emit('categoryClicked', tagId);
}

onMounted( async () => {
   tags.value = await getTagsAsync();
})

const emit = defineEmits<{
    (e: 'categoryClicked', categoryId: number):void
}>();


</script>

<template>
    <div class="sidebar-container" v-bind:class="{'open border-sidebar': sideMenuOpen}">
        <button class="button-basic sidebar-toggle button-menu border-menu" v-on:click="sideMenuOpen = !sideMenuOpen">
            {{ sideMenuOpen ? '◂' : '▸' }}
        </button>
        <h2 class="title">Our Menu</h2>
        <ul class="category-list ul-reset">
            <li 
            class="category border-menu popout" 
            v-bind:class="activeCategory === category.id ? 'active-highlight-vertical-line' : ''"
            v-for="tag in tags" :key="tag.id" 
            v-on:click="handleTagClicked(tag.id)">
                {{ tag.name }}
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
    opacity: 0.9;
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
        top: 25%;

        background-color: white;

        padding: 1rem;
        margin-left: 2px;

        transform: translateX(-170px); /* mostly hidden, leave button peeking out */
        transition: transform 0.3s ease;
        z-index: 997;
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
