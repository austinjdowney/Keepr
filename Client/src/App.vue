<template>
  <header>
    <div class="ml-4">
      <i class="fas fa-key text logo-icon">PER</i>
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
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

.logo-icon{
  height: 2rem;
  width: 2rem;
  background-image: radial-gradient($primary-light, $primary);
}
</style>
