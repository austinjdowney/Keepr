<template>
  <header>
    <div class="row bg-secondary">
      <div class="
         col-6 mx-3"
      >
        <router-link :to="{ name: 'HomePage' }" class="nav-link">
          <i class="fas fa-key text-primary fa-2x ml-3"></i>
          <span class="mx-1 text-primary"><strong>PER</strong></span>
        </router-link>
      </div>
      <div class="col-4">
        <MyNavbar />
      </div>
    </div>
  </header>
  <main>
    <router-view />
  </main>
  <footer>
    <div class="bg-secondary text-light text-center p-4">
      <a class="github-link" href="http://www.github.com/austinjdowney">         <i class="fas fa-key text-light fa-lg"></i>
        Austin Downey
        <i class="fab fa-github text-light fa-lg" aria-hidden="true"></i>
      </a>
    </div>
  </footer>
</template>

<script>
// import { computed, reactive } from 'vue'
// import { AppState } from './AppState'
// import { AuthService } from '../services/AuthService'

export default {
  name: 'App',
  setup() {

  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.github-link{
  color: var(--off-white);
}

</style>
