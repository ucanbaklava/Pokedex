<template>
    <div class="container m-auto w-3/4 h-full min-h-screen">
      <the-search-panel class="pt-10 w-full" />
      <div class="flex flex-col w-full p-1" v-if="detailedSearchResults.length > 0">
        <pokemon-list-single v-for="(pokemon, index) in detailedSearchResults" :key="index" :pokemon="pokemon" class="m-1" />
      </div>
    </div>
</template>

<script>
import PokemonListSingle from './Pokemon/PokemonListSingle.vue'
import TheSearchPanel from './TheSearchPanel.vue'
import {mapGetters} from 'vuex'
export default {
  name: 'Home',
  computed:{
    ...mapGetters(['detailedSearchResults'])
  },
  props: {
  },
  components:{
    TheSearchPanel,
    PokemonListSingle
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
