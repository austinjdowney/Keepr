<template>
  <div class="keep col-4">
    <div>
      <img :src="keeps.img" alt="Keep's Picture" class="position-relative">
      <div>
        <p>{{ keeps.name }}</p>
        <img :src="keeps.creator.picture" alt="" class="keeps-creator">
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import Notification from '../utils/Notification'
export default {
  name: 'Keep',
  props: {
    keeps: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    const state = reactive({
      newKeep: {},
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    return {
      state,
      route,
      async deleteKeep() {
        try {
          if (await Notification.confirmACtion('Are you sure?', "You won't be able to revert this!", 'warning', 'Yes,Remove Keep')) {
            await keepsService.deleteKeep(props.keeps.id, state.account.id)
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

</style>
