<template>
  <div v-if="state.loading === true">
    Loading...
  </div>
  <div v-else class="container-fluid">
    <div class="row">
      <div class="col-md-12">
        <div class="card-columns mt-3">
          <Keep v-for="keeps in state.keeps" :key="keeps.id" :keeps="keeps" />
        </div>
      </div>
      <!-- <KeepDetailsModal /> -->
      <KeepDetailsModal />

      <!-- Injecting All Keeps -->
    </div>
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
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
// .home{
//   text-align: center;
//   user-select: none;
//   > img{
//     height: 200px;
//     width: 200px;
//   }
// }
body {
  margin: 0;
  padding: 0rem;
}

.masonry-with-flex {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  max-height: 400px;
  img {
    width: 200px;
    margin: 0 2rem 2rem 0rem;
  }
  @for $i from 1 through 100 {
    div:nth-child(#{$i}) {
      $h: (random(400) + 100) + px;
      height: $h;
      line-height: $h;
    }
  }
}
</style>
