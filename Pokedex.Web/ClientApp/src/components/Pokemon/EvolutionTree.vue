<template>
  <span class="text-3xl font-bold mb-10">Pokémon Evolution</span>
  <div
    class="flex justify-center items-center"
    v-if="currentPokemon != null && evolutionTree != null"
  >
    <div class="flex flex-row items-center" v-for="item in evolutionTree" :key="item.id">
      
      <div class="w-30 block text-center" v-if="item.evolution_type === 'level-up'">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 350 100" height="15px" class="m-auto">
          <defs>
            <marker id="arrowhead" markerWidth="10" markerHeight="7" 
          refX="0" refY="3.5" orient="auto">
              <polygon points="0 0, 10 3.5, 0 7" />
            </marker>
          </defs>
          <line x1="0" y1="50" x2="250" y2="50" stroke="#000" stroke-width="8" 
          marker-end="url(#arrowhead)" marker-start="url(#arrowhead)" />
        </svg>        
        <span class="" ><small>Level ({{item.minimum_level}})</small></span>
      </div>

      <div class="w-30 block text-center" v-if="item.evolution_type === 'trade'"  height="15px" >
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 350 100"  class="m-auto">
          <defs>
            <marker id="arrowhead" markerWidth="10" markerHeight="7" 
          refX="0" refY="3.5" orient="auto">
              <polygon points="0 0, 10 3.5, 0 7" />
            </marker>
          </defs>
          <line x1="0" y1="50" x2="250" y2="50" stroke="#000" stroke-width="8" 
          marker-end="url(#arrowhead)" marker-start="url(#arrowhead)" />
        </svg>        
        <span class="" ><small>{{normalize(item.evolution_type)}}</small></span>
      </div>   


      <div class="w-30 block text-center" v-if="item.evolution_type === 'use-item'">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 350 100"  height="15px"  class="m-auto">
          <defs>
            <marker id="arrowhead" markerWidth="10" markerHeight="7" 
          refX="0" refY="3.5" orient="auto">
              <polygon points="0 0, 10 3.5, 0 7" />
            </marker>
          </defs>
          <line x1="0" y1="50" x2="250" y2="50" stroke="#000" stroke-width="8" 
          marker-end="url(#arrowhead)" marker-start="url(#arrowhead)" />
        </svg>        
        <span class="" ><small>Use {{normalize(item.item)}}</small></span>
      </div>   

      <div class="w-30 block text-center">
        <a href=""
          @click.prevent="
            $router.push({
              name: 'Pokemon',
              params: { identifier: item.identifier },
            })
          "
          >
          <img :src="require(`../../assets/sprite/${item.identifier}.png`)">
</a>
        <span class="infocard-lg-data text-muted">
          <small>#{{ item.id.toString().padStart(3, "0") }}</small>
          <br />
          <router-link class="pokemon-link" :to="{name: Pokemon, params: {identifier: item.identifier}}"  :href="`/pokemon/${item.identifier}`">{{
            normalize(item.identifier)
          }}</router-link
          ><br />
          <small v-for="(type, index) in item.pokeType" :key="index">
            {{ index > 0 ? "-" : "" }}
            <a :href="`/pokemon/${type.identifier}`" :class="`type-${type.identifier}`" class="font-bold">{{
              normalize(type.identifier)
            }}</a>
          </small>
        </span>
      </div>
      <div class="w-30 block text-center" v-if="item === null">
          <span><i>{{normalize(item.identifier)}}</i> does not evolve</span>
      </div>

    </div>
  </div>
</template>

<script>
import stringFilters from "../../filters/stringFilters";
import arrayFilters from "../../filters/arrayFilters"
import { mapGetters } from "vuex";
export default {
  name: "EvolutionTree",
  data() {
    return {
      test: null,
    };
  },
  computed: {
    ...mapGetters(["currentPokemon", "evolutionTree"]),
  },
  methods: {
    normalize(text) {
      return stringFilters.normalizeText(text);
    },
    sortArrayAsc(item) {
        return arrayFilters.sortByAsc(item)
    }
  },
};
</script>
