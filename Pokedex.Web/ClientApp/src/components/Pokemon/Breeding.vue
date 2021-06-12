<template>
    <span class="text-3xl font-bold mb-10">Breeding</span>
    <table>
      <tbody>
        <tr>
          <th>Egg Groups:</th>
          <td>
            {{ currentGender.identifier[0] }}
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
</template>

<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters(["currentGender"]),
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
