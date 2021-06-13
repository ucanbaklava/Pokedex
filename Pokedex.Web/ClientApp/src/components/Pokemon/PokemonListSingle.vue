<template>
  <div class="flex flex-row w-full bg-gray-100 shadow border justify-evenly items-center" >
    <div class="w-1/12">
        <img :src="getImage(pokemon.identifier)" alt="" />
    </div>
    <div class="flex flex-col w-2/12">
        <router-link :to="{name: 'Pokemon', params: {identifier: pokemon.identifier}}">{{normalize(pokemon.identifier)}}</router-link>
        <span>#{{pokemon.id}}</span>
    </div>
    <div class="flex flex-col w-1/12">
        <pokemon-type-tag v-for="type in pokemon.pokeTypes" :key="type.id" :type="type.identifier" />
    </div>

    <div class="block w-1/12 hidden md:block">
        <router-link v-for="(ability, index) in pokemon.abilities.filter(x => !x.is_hidden)"   class="pokemon-link block" 
            :key="index" :to="{name: 'SingleAbility', params: { identifier: ability.identifier }}" >{{normalize(ability.identifier)}}
        </router-link>
    </div>
    <div v-if="pokemon.abilities.filter(x => x.is_hidden).length > 0" class="w-1/12 hidden md:block">
        <router-link v-for="(ability, index) in pokemon.abilities.filter(x => x.is_hidden)"  class="pokemon-link block"
            :key="index" :to="{name: 'SingleAbility', params: { identifier: ability.identifier }}" >{{normalize(ability.identifier)}}
        </router-link>
    </div>
    <div v-else class="w-1/12 hidden md:block">-</div>     
    <div class="xl:flex flex-row w-4/12 text-xs hidden">
        <div v-for="(stat, index) in pokemon.pokemonStats" :key="index" class="flex flex-col p-3">
            <span class="font-bold text-gray-500">{{normalize(stat.identifier)}}</span>
            <span>{{stat.base_stat}}</span>    
        </div>
    </div>   
  </div>
</template>

<script>
import PokemonTypeTag from './PokemonTypeTag.vue'
import stringFilters from '../../filters/stringFilters'
export default {
  props: ["pokemon"],
  components: {
      PokemonTypeTag
  },
  methods: {
    getImage(identifier) {
      try {
        return require(`@/assets/sprite/${identifier}.png`);
      } catch (e) {
        return require(`@/assets/pokemon.png`);
      }
    },
    normalize(text) {
        return stringFilters.normalizeText(text)
    }
  },
};
</script>
