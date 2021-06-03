<template>
  <div class="vaultPage container-fluid" v-if="state.activeVault">
    <div class="row">
      <div class="col-12 ml-3">
        <h1>{{ state.activeVault.name }}</h1>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <div class="card-columns">
          <Keep v-for="keeps in state.keeps" :key="keeps.id" :keeps="keeps" />

          <!-- injecting VaultKeeps.. keeps for this vault -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { vaultsService } from '../services/VaultsService'
import Notification from '../utils/Notification'

export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    const state = reactive({
      vault: computed(() => AppState.vault),
      activeVault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps)
      // vaultkeeps or keeps           ^^
    })
    onMounted(async() => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await vaultKeepsService.getKeepsByVaultId(route.params.id)
      } catch (error) {
        Notification.toast('Error: ' + error, 'warning')
      }
    })
    return {
      route,
      state
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
