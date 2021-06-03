<template>
  <div v-if="state.activeProfile">
    <div v-if="state.loading === true">
      Loading...
    </div>
    <div v-else class="profilePage container-fluid">
      <div class="row">
        <div class="ml-5 my-5">
          <img :src="state.activeProfile.picture" alt="Profile Picture">
          <div>
            <p>{{ state.activeProfile.name }}</p>
          </div>
          <p>
            Vaults: {{ state.vaults.length }}
          </p>
          <p>
            Keeps: {{ state.keeps.length }}
          </p>
        </div>
      </div>
      <div class="row">
        <div class="ml-5">
          <h5>Vaults</h5>
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
        <div class="row">
          <div class="col-12">
            <div class="card-columns">
              <Vault v-for="vaults in state.vaults" :key="vaults.id" :vaults="vaults" />
            </div>
          </div>
        </div>
      <!-- injecting each vault -->
      </div>
      <div class="row">
        <div class="ml-5">
          <h5>Keeps</h5>
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
        <div class="row">
          <div class="col-12">
            <div class="card-columns">
              <Keep v-for="keeps in state.keeps" :key="keeps.id" :keeps="keeps" />
            </div>
          </div>
        </div>
      </div>
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
        await vaultsService.getVaultsByProfileId(route.params.id)
        // vaultskeep??
      }
    }
    )
    const state = reactive({
      loading: true,
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      activeProfile: computed(() => AppState.activeProfile),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    onMounted(async() => {
      // route.params.id in the parameters?
      await profilesService.getProfileById()
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
