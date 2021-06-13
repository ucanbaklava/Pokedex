<template>
<div v-if="currentGender">
    <span class="text-3xl font-bold mb-10">Breeding</span>
    <table>
      <tbody>
        <tr>
          <th>Egg Groups:</th>
          <td>
            {{ normalize(currentGender.identifier[0]) }}
          </td>
        </tr>
        <tr>
          <th>Gender:</th>
          <td>
            {{currentGender.gender_type}}
          </td>
        </tr>
        <tr>
          <th>Egg Cycles:</th>
          <td>
            {{ currentGender.hatch_counter }}
          </td>
        </tr>
      </tbody>
    </table>
</div>    
</template>

<script>
import { mapGetters } from "vuex";
import stringFilters from '../../filters/stringFilters'
export default {
  computed: {
    ...mapGetters(["currentGender"]),
  },
  methods: {
    normalize(text) {
      return stringFilters.normalizeText(text)
    }
  }
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
