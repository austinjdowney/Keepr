<template>
  <header>
    <div class=" ml-4 my-3">
      <i class="fas fa-key text text-primary fa-lg">PER</i>
    </div>
    <div>
      <div class="buttons d-flex justify-content-around mb-3">
        <span class="navbar-text">
          <button
            class="btn btn-outline-primary text-uppercase"
            @click="login"
            v-if="!user.isAuthenticated"
          >
            Login
          </button>
        </span>
        <span class="navbar-text">
          <button
            class="btn btn-outline-primary text-uppercase"
            @click="signUp"
            v-if="!user.isAuthenticated"
          >
            Sign Up
          </button>
        </span>
      </div>
    </div>
    <MyNavbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer>
    <div class="bg-dark text-light text-center p-4">
      Made with 💖 by <a class="github-link" href="http://www.github.com/austinjdowney">
        Austin Downey
      </a>
    </div>
  </footer>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from './AppState'
import { AuthService } from '../services/AuthService'

export default {
  name: 'App',
  setup() {
    const state = reactive({
      dropOpen: false
    })
    return {
      state,
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async signUp() {
        AuthService.loginWithRedirect({
          screen_hint: 'signup'
        })
      },
      async logout() {
        await AuthService.logout({ returnTo: window.location.origin })
      },
      activeProfile() {
        AppState.activeProfile = state.account
      }
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

</style>
