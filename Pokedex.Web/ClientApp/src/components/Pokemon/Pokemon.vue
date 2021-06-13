<template>
  <div v-if="currentPokemon">
    <section
      class="flex flex-row justify-center items-center shadow border h-24"
      :class="`hero-${currentPokemon.pokeTypes[0].identifier}`"
    >
      <div class="">
        <div class="text-white font-bold">
          <!---->
          <h1 class="text-2xl">{{ normalize(currentPokemon.identifier) }}</h1>
          <h2 class="text-center">#{{ currentPokemon.id }}</h2>
        </div>
        <!---->
      </div>
    </section>
    <div class="flex flex-col items-center">
      <div>
        <img
          :src="getImage(currentPokemonId)"
          class="p-3"
        />
      </div>
      <flavor-text class="w-11/12" />

      <div class="flex flex-row w-11/12  flex-wrap	justify-around	">
        <div class="md:w-2/5 w-full poke-data-box">
          <pokedex-data />
        </div>

        <div class="md:w-2/5 w-full poke-data-box">
          <stats />
        </div>
        <div class="w-full poke-data-box">
          <evolution-tree />
        </div>
        <div class="md:w-2/5 w-full poke-data-box">
          <breeding />
        </div>
        <div class="w-full poke-data-box">
          <moves />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import PokedexData from "./PokedexData.vue";
import EvolutionTree from "./EvolutionTree.vue";

import { store } from "../../store";
import { mapGetters } from "vuex";

import stringFilters from "../../filters/stringFilters";
import Stats from "./Stats.vue";
import FlavorText from "./FlavorText.vue";
import Breeding from "./Breeding.vue";
import Moves from './Moves.vue';

export default {
  name: "Pokemon",
  data() {
    return {
      defaultImg: 'require("@/assets/pokemon.png")',
    };
  },
  components: {
    PokedexData,
    EvolutionTree,
    Stats,
    FlavorText,
    Breeding,
    Moves,
  },
  methods: {
    normalize(text) {
      return stringFilters.normalizeText(text);
    },
    getImage(id) {

      try {
        return require(`@/assets/pokemon/${id}.png`)
      } catch (e) {
        return require(`@/assets/pokemon.png`)
      }
    },
  },
  computed: {
    ...mapGetters([
      "currentPokemonName",
      "currentPokemonId",
      "currentPokemon",
      "is_pokemon_loaded",
    ]),
  },
  async beforeRouteUpdate(to, from, next) {
    store.dispatch("getEvolutionTreeByIdentifier", to.params.identifier);
    store.dispatch("getStatsByIdentifier", to.params.identifier);
    store.dispatch("getGenderByIdentifier", to.params.identifier);
    store.dispatch("getFlavorTextByIdentifier", to.params.identifier);
    store.dispatch("getAbilitiesByLevel", to.params.identifier);

    await store
      .dispatch("getPokemonByIdentifier", to.params.identifier)
      .then(() => next());
  },
  async beforeRouteEnter(to, from, next) {
    store.dispatch("getEvolutionTreeByIdentifier", to.params.identifier);
    store.dispatch("getStatsByIdentifier", to.params.identifier);
    store.dispatch("getGenderByIdentifier", to.params.identifier);
    store.dispatch("getFlavorTextByIdentifier", to.params.identifier);
    store.dispatch("getAbilitiesByLevel", to.params.identifier);

    await store
      .dispatch("getPokemonByIdentifier", to.params.identifier)
      .then(() => next());
  },
};
</script>
