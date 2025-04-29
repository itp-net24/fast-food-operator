import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import MenuView from '../views/MenuView.vue'
import KitchenDisplay from '../views/KitchenDisplay.vue'
import DisplayOrderNumber from '../views/DisplayOrderNumber.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue'),
    },
    {
      path: '/menu',
      name: 'menu',
      component: MenuView,
    },
    {
      path: '/kitchen-display',
      name: 'kitchen-display',
      component: KitchenDisplay,
    },
    {
      path: '/display-order',
      name: 'display-order',
      component: DisplayOrderNumber,
    }

  ],
})

export default router
