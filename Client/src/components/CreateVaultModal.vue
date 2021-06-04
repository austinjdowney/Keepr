<template>
  <div class="modal"
       id="new-vault-form"
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
            <div class="ml-4 mt-4">
              <b>Create Vault</b>
            </div>
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close Modal">
            <span class="exit-modal-icon" aria-hidden="true">&times;</span>
          </button>
        </div>
        <form @submit.prevent="createVault">
          <div class="modal-body">
            <div class="row">
              <div class="col-12">
                <div class="form-group">
                  <label for="name">Vault Name:</label>
                  <input type="text"
                         class="form-control"
                         id="keepName"
                         placeholder="Keep Name..."
                         v-model="state.newVault.name"
                         required
                  >
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-12">
                <div class="form-group">
                  <label for="description">Description</label>
                  <textarea class="form-control input-box locked-scroll"
                            rows="3"
                            wrap="hard"
                            placeholder="Description..."
                            minlength="3"
                            v-model="state.newVault.description"
                            id="description"
                  >
              </textarea>
                </div>
              </div>
              <div class="col-12">
                <div class="form-group">
                  <label for="image">Img Url:</label>
                  <input type="text"
                         class="form-control"
                         id="imgUrl"
                         placeholder="Img Url..."
                         v-model="state.newVault.img"
                  >
                </div>
              </div>
              <div class="col-12">
                <div class="mt-2">
                  <input type="checkbox" class="" v-model="state.newVault.isPrivate">
                  <label class="" for="exampleCheck1">Is this Private?</label>
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="submit" class="btn btn-grad-modal" title="Crate Vault">
              Create
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import Notification from '../utils/Notification'
import { useRoute } from 'vue-router'
import $ from 'jquery'
export default {
  name: 'CreateVaultModal',
  props: {
  },
  setup() {
    const route = useRoute()
    const state = reactive({
      newVault: {},
      vaults: computed(() => AppState.vaults),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    return {
      route,
      state,
      async createVault() {
        try {
          await vaultsService.createVault(state.newVault)
          state.newVault = {}
          $('#new-vault-form').modal('hide')
          Notification.toast('Successfully Created Vault', 'success')
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
@import '../assets/scss/_variables.scss';
@import "../assets/scss/main.scss";

.btn-grad-cancel:hover {
  background-position: right center;
  /* change the direction of the change here */
  color: #fff;
  text-decoration: none;
}

.locked-scroll{
  height:20vh;
  overflow-y: scroll;
  word-break: break-all;
}
</style>
