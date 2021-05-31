import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard } from '@bcwdev/auth0provider-client'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    // url path

    path: '/',

    // name=name on the page file name
    name: 'HomePage',
    component: loadPage('HomePage')
  },
  {
    path: '/about',
    name: 'AboutPage',
    component: loadPage('AboutPage')
  },
  {
    path: '/account',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  }, {
    path: '/profiles/:id',
    name: 'ProfilePage',
    component: loadPage('ProfilePage')
  },
  {
    path: '/vaults/:id',
    name: 'VaultDetailsPage',
    component: loadPage('VaultDetailsPage')
  }
]

const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})

export default router
