<template>
  <div v-if="state.loading === true">
    Loading...
  </div>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <!-- Injecting All Keeps -->
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'HomePage',
  setup() {
    const state = reactive({
      loading: true,
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      await keepsService.getAllKeeps()
      state.loading = false
    })
    return {}
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
