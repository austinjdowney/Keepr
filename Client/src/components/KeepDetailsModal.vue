<template>
  <div v-if="state.loading === true">
    Loading...
  </div>
  <div class="keepDetailsModal">
    <div class="modal"
         id="keep-details-modal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="exampleModalLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <img class="modal-logo" src="" alt="">
            <!-- find imagelogo.. or use header logo -->
            <h5 class="modal-title" id="exampleModalLabel">
              <i class="fas fa-key text-primary fa-2x ml-3"></i>
              <span class="mx-1 text-primary"><strong>PER</strong></span>
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span class="exit-modal-icon" aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="row">
              <div class="col-6 image-fluid">
                <img :src="state.activeKeep.img" alt="">
              </div>
              <div class="row">
                <div class="keeps-name">
                  {{ state.activeKeep.name }}
                </div>
                <span v-if="state.activeKeep.creator">
                  <img :src="state.activeKeep.creator.picture" alt="" class="keeps-creator rounded-circle">
                </span>
              </div>
              <div class="col-6">
                <div class="row">
                  <div class="col-12 d-flex justify-content-between ml-3">
                    <i class="far fa-eye" title="number of views">{{ state.activeKeep.views }}</i>
                    <i class="fas fa-key" title="number of keeps">{{ state.activeKeep.keeps }}</i>
                    <i class="fas fa-share-square" title="number of shares">{{ state.activeKeep.shares }}</i>
                  </div>
                  <div class="col-12 d-flex justify-content-center">
                    <h3>
                      {{ state.activeKeep.name }}
                    </h3>
                  </div>
                  <div class="col-12 d-flex justify-content-center">
                    {{ state.activeKeep.description }}
                  </div>
                </div>
              <!-- views/keeps/shares -->
              </div>
            </div>
          </div>
          <div class="modal-footer" v-if="state.user.isAuthenticated">
            <div class="row">
              <div class="col-12">
                <div class="dropdown">
                  <label class="mr-1">Select Your Vault</label>
                  <select @click="createVaultKeep"
                          class="form-select"
                          aria-labelledby="dropdownMenuButton"
                          style="border: 1px gray solid;"
                          v-model="state.newVaultKeep.vaultId"
                          required
                  >
                    <option v-for="vault in state.vaults" :key="vault.id" :value="vault.id">
                      {{ vault.name }}
                    </option>
                  </select>
                </div>
                <div class="row">
                  <button @click="deleteKeep"
                          type="button"
                          v-if="state.account.id === state.activeKeep.creatorId"
                          class="btn btn-grad-modal"
                          data-dismiss="modal"
                  >
                    Delete
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import Notification from '../utils/Notification'
import { keepsService } from '../services/KeepsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import $ from 'jquery'

export default {
  name: 'KeepDetailsModal',
  props: {
    keeps: {
      type: Object,
      required: true
    }
  },
  setup() {
    const route = useRoute()
    const state = reactive({
      newVaultKeep: {},
      activeKeep: computed(() => AppState.activeKeep),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      loading: true
    })
    onMounted(async() => {
      // route.params.id in the parameters?
      await keepsService.getKeepById(route.params.id)
      state.loading = false
    })
    return {
      route,
      state,
      async createVaultKeep() {
        try {
          state.newVaultKeep.keepId = state.activeKeep.id
          await vaultKeepsService.createVaultKeep(state.newVaultKeep)
          $('#keep-details-modal').modal('hide')
          Notification.toast('Successfully Added To Vault', 'success')
          state.newVaultKeep = {}
        } catch (error) {
          Notification.toast('Error: ' + error, 'warning')
        }
      },
      async deleteKeep() {
        try {
          if (await Notification.confirmAction('Are you sure?', "You won't be able to revert this!", 'warning', 'Yes, Remove Keep')) {
            await keepsService.deleteKeep(state.activeKeep.id, state.account.id)
          }
        } catch (error) {
          Notification.toast('Error: ' + error, 'warning')
        }
      }
    }
  },
  components: {}
}

</script>

<style lang="scss" scoped>
img{
  width: 100%;
}
.keeps-name{
  position:absolute;
  bottom:-3.5rem;
  left:4rem;
  font-weight: bold;
  font-size:15px;
}
.keeps-creator{
  position:absolute;
  width: 50px;
  left:.5rem;
  bottom:-3rem;
}

</style>
