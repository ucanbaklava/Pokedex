<template>
  <div v-if="moveDetail">
    <section
      class="flex flex-row justify-center items-center shadow border h-24"
      :class="`hero-${moveDetail.type_identifier}`"
    >
      <div class="">
        <div class="text-white font-bold">
          <!---->
          <h1 class=" ">
            <i>{{ normalize($route.params.identifier) }}</i>
          </h1>
        </div>
        <!---->
      </div>
    </section>
    <div class="w-full">
      <h1 class="text-5xl font-bold mb-10"></h1>
      <table>
        <tbody>
          <tr>
            <th>Flavor Text</th>
            <td>
              <strong>{{ moveDetail.flavor_text }}</strong>
            </td>
          </tr>
          <tr>
            <th>Type</th>
            <td>
              <pokemon-type-tag :type="moveDetail.type_identifier" />
            </td>
          </tr>
          <tr>
            <th>Category</th>
            <td>
              <strong>{{ moveDetail.cateogory_identifier }}</strong>
            </td>
          </tr>
          <tr>
            <th>Power</th>
            <td>
              <strong>{{ moveDetail.power }}</strong>
            </td>
          </tr>
          <tr>
            <th>Accuracy</th>
            <td>
              <strong>{{ moveDetail.accuracy }}</strong>
            </td>
          </tr>
          <tr>
            <th>PP</th>
            <td>
              <strong>{{ moveDetail.pp }}</strong>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import stringFilters from "../../filters/stringFilters";
import PokemonTypeTag from "./PokemonTypeTag.vue";
export default {
  components: { PokemonTypeTag },
  computed: {
    ...mapGetters(["moveDetail"]),
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
