<template>
  <div class="vault body image-fluid">
    <div>
      <router-link :to="{ name: 'VaultPage', params: { id: vaults.id }}">
        <img :src="vaults.img" alt="Vault's Picture" class="vaults-background image-fluid">
      </router-link>
      <div>
        <p>
          {{ vaults.name }}
          <img :src="vaults.creator.picture" alt="" class="vaults-creator rounded-circle">
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Notification from '../utils/Notification'
export default {
  name: 'Vault',
  props: {
    vaults: {
      type: Object,
      required: true
    }
  },
  setup() {
    const route = useRoute()
    const state = reactive({
      vault: computed(() => AppState.vaults),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    return {
      state,
      route,
      async deleteVault() {
        try {
          if (await Notification.confirmAction('Are you sure?', "You won't be able to revert this!", 'warning', 'Yes,Remove Vault')) {
            await vaultsService.deleteVault(state.vault.id, state.account.id)
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
  width: 200px;
}
// .keeps-background{
//   // position:relative;
// }
.vaults-creator{
  width: 50px;
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
