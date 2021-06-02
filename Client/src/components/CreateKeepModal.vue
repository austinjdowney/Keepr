<template>
  <div class="modal"
       id="new-keep-form"
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
              <b>Create Keep</b>
            </div>
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span class="exit-modal-icon" aria-hidden="true">&times;</span>
          </button>
        </div>
        <form @submit.prevent="createKeep">
          <div class="modal-body">
            <div class="row">
              <div class="col-12">
                <div class="form-group">
                  <label for="name">Keep Name:</label>
                  <input type="text"
                         class="form-control"
                         id="keepName"
                         placeholder="Keep Name..."
                         v-model="state.newKeep.name"
                         required
                  >
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-12">
                <div class="form-group">
                  <label for="description">Description:</label>
                  <input type="text"
                         class="form-control"
                         id="description"
                         placeholder="Description..."
                         rows:
                         3
                         v-model="state.newKeep.description"
                  >
                </div>
              </div>
              <div class="col-12">
                <div class="form-group">
                  <label for="image">Img Url:</label>
                  <input type="text"
                         class="form-control"
                         id="imgUrl"
                         placeholder="Img Url..."
                         v-model="state.newKeep.Img"
                  >
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="submit" class="btn btn-grad-modal">
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
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import Notification from '../utils/Notification'
import { useRoute } from 'vue-router'
import $ from 'jquery'
export default {
  name: 'CreateKeepModal',
  props: {
  },
  setup() {
    const route = useRoute()
    const state = reactive({
      newKeep: {},
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    return {
      route,
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state.newKeep)
          state.newKeep = {}
          $('#new-keep-form').modal('hide')
          Notification.toast('Successfully Created Keep', 'success')
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
</style>
