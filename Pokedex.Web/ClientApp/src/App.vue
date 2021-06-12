<template>
      <the-navbar></the-navbar>

  <div class="container m-auto bg-white">
      <router-view />
  </div>
</template>

<script>
import TheNavbar from './components/TheNavbar.vue'

export default {
  name: 'App',
  components: {
    TheNavbar
  },
  mounted(){
  },
  computed: {
    currentPokemon() {
      console.log(this)
      return this.$store.state.currentPokemon
    }
  }
}
</script>

<style>

</style>
