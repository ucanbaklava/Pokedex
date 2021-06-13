<template>
  <div class="flex flex-col  search-bar">
    <div class="relative text-gray-600 focus-within:text-gray-400">
      <span class="absolute inset-y-0 left-0 flex items-center pl-2">
        <div
          class="p-1 focus:outline-none focus:shadow-outline"
        >
          <svg
            fill="none"
            stroke="currentColor"
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            viewBox="0 0 24 24"
            class="w-6 h-6"
          >
            <path d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path>
          </svg>
        </div>
      </span>
      <input
        type="search"
        @input="$store.dispatch('searchPokemonDetailed', searchString)"
        class=" md:w-full w-full p-3 border shadowpy-2  text-white bg-gray-900 rounded-md pl-10 "
        placeholder="Search Pokémon"
        autocomplete="off"
        v-model="searchString"

      />
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      searchString: '',
    };
  },
};
</script>

<style scoped>

</style>
