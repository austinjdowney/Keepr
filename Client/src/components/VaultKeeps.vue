<template>
  <div class="vaultKeeps body">
    <div class="card">
      <div v-if="state.account.id == state.activeVault.creatorId">
        <i @click="deleteVaultKeep" class="fa fa-trash" aria-hidden="true" title="delete vaultkeep"></i>
      </div>
      <div @click="activeKeep"
           data-toggle="modal"
           data-target="#keep-details-modal"
           class="image-fluid"
      >
        <img :src="keeps.img"
             alt="Keep's Picture"
             class="keeps-background image-fluid"
        >
      </div>
      <div>
        <p>
          <router-link :to="{ name: 'ProfilePage', params: { id: keeps.creatorId } }">
            <div class="keeps-name">
              {{
                keeps.name
              }}
            </div>
            <div>
              <img
                :src="keeps.creator.picture"
                alt="keeps creator picture"
                class="keeps-creator rounded-circle"
              >
            </div>
          </router-link>
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
// import { keepsService } from '../services/KeepsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import Notification from '../utils/Notification'
export default {
  name: 'VaultKeeps',
  props: {
    keeps: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      activeVault: computed(() => AppState.activeVault)
    })
    return {
      state,
      route,
      async deleteVaultKeep() {
        try {
          if (await Notification.confirmAction('Are you sure?', "You won't be able to revert this!", 'warning', 'Yes,Remove Keep from Vault')) {
            // await vaultKeepsService.deleteVaultKeep(state.vaultKeeps, state.vaults.id)
            await vaultKeepsService.deleteVaultKeep(props.keeps.vaultKeepId, state.vaults.id)
          }
        } catch (error) {
          Notification.toast('Error: ' + error, 'warning')
        }
      // },
      // async getVaultKeepById() {
      //   state.vaultKeeps = props.keeps
      //   await vaultKeepsService.getKeepsByVaultId(state.vaults.id)
      // }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
img{
  width: 100%;
}
.keeps-background{
  position:relative;
}
.keeps-creator{
  position:absolute;
  width: 40px;
  right:1rem;
  bottom:.5rem;
}
.keeps-name{
  position:absolute;
  bottom:1rem;
  left:1.5rem;
   font-weight: bold;
  font-size:25px;
  color: white;
  text-shadow: 2px 4px 4px black;
}
// body {
//   margin: 0;
//   padding: 1rem;
// }

// .masonry-with-flex {
//   display: flex;
//   flex-direction: column;
//   flex-wrap: wrap;
//   max-height: 400px;
//   div {
//     width: 200px;
//     margin: 1rem 2rem 2rem 1rem;
//   }
//   @for $i from 1 through 36 {
//     div:nth-child(#{$i}) {
//       $h: (random(400) + 100) + px;
//       height: $h;
//       line-height: $h;
//     }
//   }
// }
</style>
