<template>
  <div>
    <h1 class="text-5xl font-bold mb-10">Pokédex Data</h1>
    <table>
      <tbody>
        <tr>
          <th>National No:</th>
          <td>
            <strong>{{ currentPokemon.id }}</strong>
          </td>
        </tr>
        <tr>
          <th>Type</th>
          <td v-if="currentPokemon !== null">
            <pokemon-type-tag
              v-for="type in currentPokemon.pokeTypes"
              :key="type.id"
              :type="type.identifier"
            ></pokemon-type-tag>
          </td>
        </tr>
        <tr>
          <th>Height</th>
          <td>
            <strong>{{ currentPokemon.height }}</strong>
          </td>
        </tr>
        <tr>
          <th>Weight</th>
          <td>
            <strong>{{ currentPokemon.weight }}</strong>
          </td>
        </tr>
        <tr>
          <th>Habitat</th>
          <td>
            <strong v-if="currentPokemon.habitat">{{
              normalize(currentPokemon.habitat)
            }}</strong>
            <strong v-else>-</strong>
          </td>
        </tr>
        <tr>
          <th>Abilities</th>
          <td>
            <router-link
              :to="{
                name: 'SingleAbility',
                params: { identifier: ability.identifier },
              }"
              :title="ability.flavor_text"
              :class="
                ability.is_hidden
                  ? 'block no-underline hover:underline text-gray-600 text-xs'
                  : 'block no-underline hover:underline text-green-600'
              "
              v-for="(ability, index) in currentPokemon.abilities"
              :key="ability.id"
            >
              {{ index + 1 + ". " + normalize(ability.identifier) }}
              <span class="text-xs" v-if="ability.is_hidden"
                >(Hidden Ability)</span
              >
            </router-link>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import PokemonTypeTag from "./PokemonTypeTag";
import stringFilters from "../../filters/stringFilters";
export default {
  computed: {
    ...mapGetters(["currentPokemon"]),
  },
  components: {
    PokemonTypeTag,
  },
  methods: {
    normalize(text) {
      return stringFilters.normalizeText(text);
    },
  },
};
</script>

<style scoped>
table {
  border-collapse: collapse;
  width: 100%;
}

tr {
  border-bottom: 1px solid #ccc;
}

th {
  text-align: left;
  color: grey;
}
</style>
