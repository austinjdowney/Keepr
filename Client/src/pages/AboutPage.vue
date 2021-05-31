<template>
  <div>
    <div v-if="state.loading === true">
      Loading...
    </div>
    <div v-else class="about">
      <h1>This is the about page</h1>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'

export default {
  name: 'AboutPage',
  setup() {
    const state = reactive({ loading: true }
    )
    return {
      state
    }
  }

}
</script>
