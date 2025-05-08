<script lang="ts" setup>
import {useAttrs, ref, watch, defineProps, defineEmits } from 'vue';

const internalShow = ref<boolean>(false);
const attrs = useAttrs()

const props = defineProps<{
    message:string
    show:boolean
}>()

const emit = defineEmits<{
    (e: 'hide'):void
}> ();

watch(() => props.show, (newVal) => {
    if (newVal) {
        internalShow.value = true;
        console.log(`status of internalShow.value ${internalShow.value}`);
        setTimeout(() => {
            internalShow.value = false;
            emit('hide');
        }, 2500) 
    }
})

</script>

<template>
    <Transition name="fade">
        <h2 v-bind="attrs" v-if="internalShow" >{{ message }}</h2>
    </Transition>
</template>

<style scoped>
    .fade-enter-active, .fade-leave-active {
    transition: opacity 0.5s ease;
    }
    .fade-enter-from, .fade-leave-to {
    opacity: 0;
    }
    .fade-enter-to, .fade-leave-from {
    opacity: 1;
    }
</style>