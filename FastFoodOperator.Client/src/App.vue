<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>
    <img alt="Claes Burgir logo" class="logo" src="@/assets/Claes_Burgir.svg" width="125" height="125" />

    <div class="wrapper">
      <nav>
        <RouterLink to="/">Home</RouterLink>
        <RouterLink to="/about">About</RouterLink>
        <RouterLink to="/menu">Menu</RouterLink>
        <RouterLink to="/display-order">Display Orders</RouterLink>
        <RouterLink to="/kitchen-display">Kitchen Display</RouterLink>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

<style scoped>

</style>
