<template>
  <div v-if="state.loading === true">
    Loading...
  </div>
  <div class="profilePage">
    <div class="row">
      <h1>{{ state.activeProfile }}</h1>
      <img :src="state.activeProfile.picture" alt="Profile Picture">
      <!-- {{state.activeProfile.picture}} -->
      <!-- {{state.activeProfile.vaults.length}} -->
      <!-- {{state.activeProfile.keeps.length}} -->
    </div>
    <div class="row">
      <div>
        <h2>Vaults</h2>
        <button title="Open Create Vault Form"
                type="button"
                class="btn btn-sm btn-grad"
                data-toggle="modal"
                data-target="#new-vault-form"
                v-if="state.account.id === route.params.id"
        >
          <i class="fa fa-plus" aria-hidden="true"></i>
        </button>
      </div>
      <!-- injecting each vault -->
    </div>
    <div class="row">
      <div>
        <h2>Keeps</h2>
        <button title="Open Create Keep Form"
                type="button"
                class="btn btn-sm btn-grad"
                data-toggle="modal"
                data-target="#new-keep-form"
                v-if="state.account.id === route.params.id"
        >
          <i class="fa fa-plus" aria-hidden="true"></i>
        </button>
      </div>
      <!-- injecting each keep -->
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'

export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    watchEffect(async() => {
      if (route.params.id) {
        await profilesService.getProfileById(route.params.id)
        await keepsService.getKeepsByProfileId(route.params.id)
        await vaultsService(route.params.id)
        // vaultskeep??
      }
    }
    )
    const state = reactive({
      loading: true,
      activeProfile: computed(() => AppState.activeProfile),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    onMounted(async() => {
      await keepsService.getKeepsByProfileId()
      await vaultsService.getVaultsByProfileId()
      state.loading = false
    })
    return {
      route,
      state

      // createKeep/vault editKeep/vault
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
