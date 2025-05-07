<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';

const scrollToTopVisible = ref<boolean>(false);

const checkScroll = () => {
  scrollToTopVisible.value = window.scrollY > 400;
}

const scrollToTop = () => {
  window.scrollTo({ top: 0, behavior: 'smooth' })
}

onMounted(() => {
  window.addEventListener('scroll', checkScroll)
})

onUnmounted(() => {
  window.removeEventListener('scroll', checkScroll)
})

</script>

<template>
    <div class="scroll-to-top" 
    v-on:click="scrollToTop"
    v-bind:class="scrollToTopVisible ? 'visible' : ''">↑</div>
</template>

<style scoped>
    .scroll-to-top {
        display:block;
        position: fixed;
        bottom: 0;
        right: 0;
        margin: 0.5rem;
        padding-inline: 1rem;
        cursor: pointer;
        opacity: 0;
        font-size: 40px;
        font-weight: 600;
        aspect-ratio: 1;
        border: var(--color-primary) solid 4px;
        color: var(--color-hover);
        background-color: rgb(255, 255, 255, 1);
        transition: opacity 0.3s ease;
        z-index: 800;

    }

    .visible {
        opacity: 0.8;
    }
</style>