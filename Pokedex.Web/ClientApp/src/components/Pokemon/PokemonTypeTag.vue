<template>
  <router-link
    v-if="type != null"
    :to="{ name: 'PokemonList', params: { identifier: type } }"
    :class="['type-list-' + type]"
    class="type-icon leading-6"
  >
    {{ normalizeString(type) }}</router-link
  >
</template>

<script>
import stringFilters from "../../filters/stringFilters";

export default {
  name: "PokemonTypeTag",
  props: ["type"],
  methods: {
    normalizeString(text) {
      return stringFilters.normalizeText(text);
    },
  },
};
</script>

<style scoped></style>
