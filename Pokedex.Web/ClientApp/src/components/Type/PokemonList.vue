<template>
    <section
      class="flex flex-row justify-center items-center shadow border h-24"
      :class="`hero-${$route.params.identifier}`"
    >
      <div class="">
        <div class="text-white font-bold">
          <!---->
          <h1 class=" ">{{ normalize($route.params.identifier) }}</h1>
        </div>
        <!---->
      </div>
    </section>
    <div v-if="pokemonListByType" class="flex flex-row flex-wrap p-5">
        <div class="flex flex-row w:full md:w-1/5  h-20 " v-for="(poke,index) in pokemonListByType" :key="index">
            <div class="poke-img">
                <a href="" @click.prevent="$router.push({name: 'Pokemon', params: { identifier: poke.identifier },})">
                    <img  :src="`https://img.pokemondb.net/sprites/sword-shield/icon/${poke.identifier}.png`" alt="Squirtle icon">
                </a>
            </div>
            <div class="flex flex-col">
                <router-link class="text-blue-600 font-semibold" :to="{ name: 'Pokemon', params: { identifier: poke.identifier } }">{{normalize(poke.identifier)}}</router-link>
                <div class="flex flex-row items-baseline">
                    <span class="text-xs text-gray-500 pr-1">#001</span>
                    <span v-for="(type,index) in poke.pokeTypes" :key="index" :class="`type-${type.identifier}`">
                        {{ index > 0 ? "-" : "" }}    
                        <router-link class="" :to="{ name: 'PokemonList', params: { identifier: type.identifier } } ">{{normalize(type.identifier)}}</router-link>
                    </span>                     
                </div>               
            </div>
        </div>
    </div>
</template>

<script>
import {mapGetters} from 'vuex'
import {store} from '../../store'
import stringFilters from '../../filters/stringFilters'
export default {
    name: 'PokemonList',
    computed: {
        ...mapGetters(["pokemonListByType"]),
    },    
    beforeRouteEnter(to, from, next){
        store.dispatch('getPokemonListByType', to.params.identifier).then(() => next())
    },
    beforeRouteUpdate(to, from, next){
        store.dispatch('getPokemonListByType', to.params.identifier).then(() => next())
    },    
    methods: {
        normalize(text) {
            return stringFilters.normalizeText(text)
        }
    }
}
</script>