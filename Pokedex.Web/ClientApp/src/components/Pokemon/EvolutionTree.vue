<template>
  <div
    class="flex justify-center items-center"
    v-if="currentPokemon != null && evolutionTree != null"
  >
    <div class="asd" v-for="item in sortArrayAsc(evolutionTree)" :key="item.id">
      <div class="w-30 block text-center">
        <a href=""
          @click.prevent="
            $router.push({
              name: 'Pokemon',
              params: { identifier: item.identifier },
            })
          "
          ><img
            class="img-fixed img-sprite"
            :src="
              `https://img.pokemondb.net/sprites/bank/normal/${item.identifier}.png`
            "
            alt="Bulbasaur sprite"
        /></a>
        <span class="infocard-lg-data text-muted">
          <small>#{{ item.id.toString().padStart(3, "0") }}</small>
          <br />
          <a class="ent-name" :href="`/pokemon/${item.identifier}`">{{
            normalize(item.identifier)
          }}</a
          ><br />
          <small v-for="(type, index) in item.pokeType" :key="index">
            {{ index > 0 ? "-" : "" }}
            <a :href="`/pokemon/${type.identifier}`" class="itype grass">{{
              normalize(type.identifier)
            }}</a>
          </small>
        </span>
      </div>
      <div class="w-30 block text-center">
        <span class=""><small>(Level 16)</small></span>
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
