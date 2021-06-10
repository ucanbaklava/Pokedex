<template>
    <div class="grid grid-cols-1 gap-4 md:grid-cols-3 flex justify-center items-center p-4"  v-if="is_pokemon_loaded">
      <div class="w-full">
          <span class="text-3xl font-bold text-center	">{{normalize(currentPokemonName)}}</span>
          <img class="object-fit m-auto p-3" :src="`https://img.pokemondb.net/artwork/${$route.params.identifier}.jpg`" />
      </div>
      <div>
        <pokedex-data  class="w-full"  />
      </div>
      <div>
        <evolution-tree />
      </div>
    </div>
</template>

<script>
import PokedexData from './PokedexData.vue'
import EvolutionTree from './EvolutionTree.vue'

import {store} from '../../store'
import {mapGetters} from 'vuex'

import stringFilters from '../../filters/stringFilters'

export default {
    name: 'Pokemon',
    components: {
        PokedexData,
        EvolutionTree
    },
    methods: {
      normalize(text) {
        return stringFilters.normalizeText(text)
      }
    },   
    computed: {
      ...mapGetters(['currentPokemonName', 'is_pokemon_loaded'])
    },
    async beforeRouteUpdate (to, from, next)  {
        store.dispatch('getEvolutionTreeByIdentifier', to.params.identifier)
        await store.dispatch('getPokemonByIdentifier', to.params.identifier).then(() => next())
    },
    async beforeRouteEnter(to, from, next) {
        store.dispatch('getEvolutionTreeByIdentifier', to.params.identifier)
        await store.dispatch('getPokemonByIdentifier', to.params.identifier ).then(() => next())
    }
       
}
</script>

