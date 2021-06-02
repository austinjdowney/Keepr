<template>
  <div class="col-4 keep body masonry-with-flex image-fluid">
    <div>
      <img :src="keeps.img" alt="Keep's Picture" class="keeps-background image-fluid">
      <div>
        <p>
          {{ keeps.name }}

          <img :src="keeps.creator.picture" alt="" class="keeps-creator rounded-circle">
        </p>
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
          if (await Notification.confirmAction('Are you sure?', "You won't be able to revert this!", 'warning', 'Yes,Remove Keep')) {
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
img{
  width: 200px;
}
// .keeps-background{
//   // position:relative;
// }
.keeps-creator{
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
