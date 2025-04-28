<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>
    
    <div class="wrapper">
      <img alt="Claes Burgir logo" class="logo" src="@/assets/Claes_Burgir.svg" width="125" height="125" />

      <h1>Admin Tools</h1>

      <nav>
        <label>
        Views:
        <RouterLink class="admin-nav-link border-menu" to="/">Home</RouterLink>
        <RouterLink class="admin-nav-link border-menu" to="/about">About</RouterLink>
        <RouterLink class="admin-nav-link border-menu" to="/menu">Menu</RouterLink>
        <RouterLink class="admin-nav-link border-menu" to="/display-order">Display Orders</RouterLink>
        <RouterLink class="admin-nav-link border-menu" to="/kitchen-display">Kitchen Display</RouterLink>
       </label>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

<style scoped>

 .wrapper {
  display:flex;
  justify-content: space-between;
  align-items: center;
  border: rgba(0, 0, 0, 0.669) solid 2px;
  background-color: rgba(175, 174, 174, 0.7);
  padding-block: 0.5rem;
 }

label {
  font-weight: 600;
}

.admin-nav-link {
  padding: 0.5rem;
  background-color: var(--color-primary);
  text-decoration: none;
  margin-right: 0.25rem;
  color: var(--color-hover);
}

h1 {
  color: black;
}
</style>
