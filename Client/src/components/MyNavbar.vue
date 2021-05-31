<template>
  <div class="navigation MyNavbar">
    <input type="checkbox" class="navigation__checkbox" id="navi-toggle">
    <label for="navi-toggle" class="navigation__button">
      <span class="navigation__icon">&nbsp;</span>
    </label>
    <div class="navigation__background">
&nbsp;
    </div>
    <span class="navbar-text">
      <button
        class="btn btn-outline-primary text-uppercase"
        @click="login"
        v-if="!user.isAuthenticated"
      >
        Login
      </button>

      <div class="row dropdown" v-else>
        <div>
          <img
            :src="user.picture"
            alt="user photo"
            height="35"
            class="rounded-circle mr-2"
          />
        </div>
        <div>
          <button
            class=" hoverable btn btn-sm btn-outline-primary text-uppercase"
            @click="logout"
          >
            logout
          </button>
        </div>
      </div>
    </span>
    <nav class="navigation__nav">
      <ul class="navigation__list" id="nav-list">
        <li class="navigation__item" @click="uncheck" id="home">
          <router-link :to="{ name: 'HomePage' }" class="navigation__link">
            Home
          </router-link>
        </li>
        <li class="navigation__item" @click="uncheck" id="about">
          <router-link :to="{ name: 'AboutPage' }" class="navigation__link">
            About
          </router-link>
        </li>
        <li @click="activeProfile" class="navigation__item" id="profile">
          <router-link :to="{ name: 'ProfilePage', params: { id: account.id } }" class="navigation__link">
            Profile
          </router-link>
        </li>
      </ul>
    </nav>
  </div>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
// import { useRouter } from 'vue-router'
export default {
  name: 'MyNavbar',
  setup() {
    // const router = useRouter()
    const state = reactive({
      dropOpen: false
    })
    // GO BACK TO HERE
    return {
      state,
      uncheck() {
        document.querySelector('.navigation__checkbox').checked = false
      },
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        await AuthService.logout({ returnTo: window.location.origin })
      },
      activeProfile() {
        AppState.activeProfile = state.account
        this.uncheck()
      }
    }
  }
}
</script>

<style lang="scss" scoped>
@import '../assets/scss/_variables.scss';
@import "../assets/scss/main.scss";
.navigation {
  font-family: $primary-font;
    &__checkbox {
        display: none;
    }
    &__button {
        background-color: #FFF;
        text-align:center;
        height: 3rem;
        width: 3rem;
        position: fixed;
        top: 3rem;
        right: 3rem;
        border-radius: 50%;
        z-index: 1002;
        box-shadow: 0 1rem 3rem rgba($dark, .2);
        cursor: pointer;
    }
    &__background {
        height: 2rem;
        width: 2rem;
        border-radius: 50%;
        position: fixed;
        top: 3rem;
        right: 3.5rem;
        background-image: radial-gradient($primary-light, $primary);
        z-index: 1000;
        transition: transform .8s cubic-bezier(0.86, 0, 0.07, 1);
    }
    &__nav {
        height: 100vh;
        position: fixed;
        top: 0;
        right: 0;
        z-index: -1001;
        opacity: 0;
        width: 0;
        transition: all .8s cubic-bezier(0.68, -0.55, 0.265, 1.55);
        display: flex;
        align-items: center;

    }
    &__list {
        list-style: none;
        width: 100%;
        display: flex;
        justify-content: center;
        flex-direction: column;
        padding-left: 0 !important;

    }
    &__item {
        margin: 1rem;
        display: flex;
        justify-content: center;

    }
    &__link {
        &:link,
        &:visited {
            display: inline-block;
            font-size: 3rem;
            font-weight: 300;
            padding: 1rem 2rem;
            color: #FFF;
            text-decoration: none;
            text-transform: uppercase;
            background-image: linear-gradient(120deg,transparent 0%,transparent 50%, #FFF 50%);
            background-size: 230%;
            transition: all .6s;
        }
        &:hover,
        &:active {
            background-position: 100%;
            color: $primary;
            transform: translateX(1rem);
        }
    }
    // NAV Functionality
    &__checkbox:checked ~ &__background {
        transform: scale(80);
    }
    &__checkbox:checked ~ &__nav {
        opacity: 1;
        width: 100%;
        z-index: 1001;
    }
    // NAV ICON
    &__icon {
        position: relative;
        margin-top: 1.3rem;
        &,
        &::before,
        &::after {
            width: 1.5rem;
            height: 2px;
            background-color: $dark;
            display: inline-block;

        }
        &::before,
        &::after {
            content: '';
            position: absolute;
            left: 0;
            transition: all .3s;
        }
        &::before {top: -.5rem}
        &::after {top: .5rem}
    }
    // &__button:hover &__icon:before {
    //     top: -1rem;
    // }
    // &__button:hover &__icon:after {
    //     top: 1rem;
    // }
    &__checkbox:checked + &__button &__icon {
        background-color: transparent;
    }
    &__checkbox:checked + &__button &__icon:before {
        top: 0;
        transform: rotate(135deg);
    }
    &__checkbox:checked + &__button &__icon:after {
        top: 0;
        transform: rotate(-135deg)
    }
}

.hide{
  display: none;
}
</style>
