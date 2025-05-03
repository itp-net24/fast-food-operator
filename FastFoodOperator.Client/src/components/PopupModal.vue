<script setup lang="ts">
  const props = defineProps<Props>();

  interface Props {
    closeOnOutsideClick?: boolean;
    enableCloseButton?: boolean;
    enableBlur?: boolean;
  }

  const emit = defineEmits<{
    (e: 'close'): void;
  }>();

  enum EventTypes{
    Close = 'close'
  }

  const closePopupWindow = () => emit(EventTypes.Close);
</script>

<template>
  <div class="backdrop" :class="{ 'background-blur': enableBlur }" @click="closePopupWindow"></div>

  <div class="popup-window">
    <button class="button-basic" id="popup-close-button" v-if="enableCloseButton" @click="closePopupWindow">X</button>

      <slot />

  </div>
</template>

<style scoped>
  #popup-close-button {
    position: absolute;
    top: 0;
    right: 0;
    transform: translate(-50%, 50%);

    z-index: 999;
    cursor: pointer;

    border: 1px solid black;
    border-radius: 50%;

    aspect-ratio: 1;
  }

  .popup-window{
    position: fixed;
    top: 50vh;
    left: 50vw;
    transform: translate(-50%, -50%);
    z-index: 999;

  }

  .background-blur {
    backdrop-filter: blur(10px);
  }

  .backdrop {
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    z-index: 998;
  }
</style>