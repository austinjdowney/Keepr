<template>
  <div class="keepDetailsModal">
    <div class="row">
      <div class="col-6 image-fluid">
        <img :src="keeps.img" alt="">
      </div>
      <div class="col-6">
        <div class="row">
          <div class="col-12">
            <i class="far fa-eye" title="number of views">{{ keeps.Views }}</i>
            <i class="fab fa-kaggle" title="number of keeps">{{ keeps.Keeps }}</i>
            <i class="fas fa-share-alt" title="number of shares"> 0 </i>
          </div>
        </div>
        <!-- views/keeps/shares -->
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'KeepDetailsModal',
  props: {
    keeps: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  },
  components: {}
}

</script>

<style lang="scss" scoped>

</style>
