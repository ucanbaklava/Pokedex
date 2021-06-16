<template>
  <div v-if="allMoves">
    <div class="overflow-x-auto">
      <div
        class="min-w-screen min-h-screen bg-gray-100 flex items-center justify-center bg-gray-100 font-sans overflow-hidden"
      >
        <div class="w-full">
          <div class="bg-white shadow-md rounded my-6">
            <table class="min-w-max w-full table-auto">
              <thead>
                <tr
                  class="bg-gray-200 text-gray-600 uppercase text-sm leading-normal"
                >
                  <th class="py-3 px-6 text-left">Name</th>
                  <th class="py-3 px-6 text-left">Type</th>
                  <th class="py-3 px-6 text-center">Category</th>
                  <th class="py-3 px-6 text-center">Power</th>
                  <th class="py-3 px-6 text-center">Acc</th>
                  <th class="py-3 px-6 text-center">PP</th>
                  <th class="py-3 px-6 text-center">Effect</th>
                </tr>
              </thead>
              <tbody class="w-full">
                <tr
                  class="border-b border-gray-200 hover:bg-gray-100"
                  v-for="(move, index) in allMoves"
                  :key="index"
                >
                  <td class="py-3 px-6 text-left whitespace-nowrap">
                    <div class="flex items-center">
                      <router-link :to="{name: 'PokemonByMove', params:{identifier: move.identifier}}"  :title="move.flavor_text">{{ normalize(move.identifier) }}</router-link>
                    </div>
                  </td>
                  <td class="py-3 px-6 text-left">
                    <div class="flex">
                      <pokemon-type-tag :type="move.type_identifier" />
                    </div>
                  </td>
                  <td class="py-3 px-6 text-center">
                    <div class="flex items-center justify-center">
                      {{ move.category_identifier }}
                    </div>
                  </td>
                  <td class="py-3 px-6 text-center">
                    <span> {{ move.power }} </span>
                  </td>
                  <td class="py-3 px-6 text-center">
                    <span> {{ move.accuracy }} </span>
                  </td>
                  <td class="py-3 px-6 text-center">
                    <span> {{ move.pp }} </span>
                  </td>
                  <td class="py-3 px-6 text-xs">
                    <span> {{ move.short_effect }} </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import PokemonTypeTag from "../Pokemon/PokemonTypeTag.vue";
import stringFilters from '../../filters/stringFilters'
import {store} from '../../store'
export default {
  components: {
    PokemonTypeTag,
  },
  computed: {
    ...mapGetters(["allMoves"]),
  },
  methods: {
      normalize(text) {
          return stringFilters.normalizeText(text)
      }
  },
  beforeRouteUpdate(to, from, next) {
    store
      .dispatch("getAllMoves")
      .then(() => next());
  },
  beforeRouteEnter(to, from, next) {
    store
      .dispatch("getAllMoves")
      .then(() => next());
  },  
};
</script>
