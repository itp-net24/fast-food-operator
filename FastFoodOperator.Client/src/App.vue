<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import {ref} from 'vue'
import CartModal from '@/components/CartModal.vue'

const adminMenuOpen = ref<boolean>(false);
const cartVisible = ref<boolean>(false);

const handleCartClick = () => {
  cartVisible.value = true;
}
</script>

<template>
  <CartModal :visible="cartVisible" @close="cartVisible = false"/>

  <header>
    <div class="wrapper">
      <img alt="Claes Burgir logo" class="logo" src="@/assets/Claes_Burgir.svg" width="125" height="125" />

      <h1>Admin Tools</h1>

      <nav class="drop-down-menu">
        <button class="button-basic admin-menu-toggle button-menu" v-on:click="adminMenuOpen = !adminMenuOpen">
        ☰ Views
        </button>

        <ul class="drop-down-list-menu ul-reset" v-bind:class="{dropDownOpen: adminMenuOpen}">
          <li class="admin-nav-link border-menu cart" @click="handleCartClick">Cart</li>
          <li><RouterLink class="admin-nav-link border-menu" to="/menu">Menu</RouterLink></li>
          <li><RouterLink class="admin-nav-link border-menu" to="/display-order">Display Orders</RouterLink></li>
          <li><RouterLink class="admin-nav-link border-menu" to="/kitchen-display">Kitchen Display</RouterLink></li>
        </ul>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

<style scoped>
.cart {
  width: min-content;
}
 .wrapper {
  display:flex;
  justify-content: space-between;
  align-items: center;
  border: rgba(0, 0, 0, 0.669) solid 2px;
  background-color: rgba(175, 174, 174, 0.7);
  padding-block: 0.5rem;
 }

.admin-nav-link {
  display: inline-block;
  width: 100%;
  padding: 0.5rem;
  background-color: var(--color-primary);
  text-decoration: none;
  white-space: nowrap;
  margin-right: 0.25rem;
  color: var(--color-hover);
}

h1 {
  color: black;
}

.admin-menu-toggle {
  display: none;
}

@media (max-width: 640px)
{
  .wrapper {
    justify-content: space-evenly;
  }
  .logo {
    display: none;
  }

  .admin-menu-toggle {
  display:block;
  }

  .drop-down-list-menu {
    display: none;
    position: absolute;
    top:95%;
    flex-direction: column;
  }
}
</style>
