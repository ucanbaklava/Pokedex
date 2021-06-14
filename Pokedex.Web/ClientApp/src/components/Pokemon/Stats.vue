<template>
    <div class="">
        <span class="text-3xl font-bold mb-10">Pokémon Stats</span>

        <div v-for="(stat, index) in currentStats" :key="index">
            <span>{{normalize(stat.identifier)}}</span>
            <div class="shadow w-full bg-grey-light">
            <div
                class="text-xs leading-none py-1 text-center text-white"
                :class="statsBarClass(stat.base_stat)"
                :style="`width: ${stat.base_stat}%`"
            >
                {{ stat.base_stat }}%
            </div>
            </div>
        </div>
    </div>  
</template>

<script>
import { mapGetters } from "vuex";
import stringFilters from "../../filters/stringFilters";
export default {
  methods: {
    statsBarClass(percentage) {
      var result;
      switch (true) {
        case percentage >= 85:
          result = "bg-green-500";
          break;
        case percentage >= 70:
          result = "bg-blue-500";
          break;
        case percentage >= 55:
          result = "bg-yellow-500";
          break;
        case percentage >= 40:
          result = "bg-purple-500";
          break;
        default:
          result = "bg-red-500";
          break;
      }
      return result;
    },
    normalize(text) {
      return stringFilters.normalizeText(text);
    },
  },
  computed: {
    ...mapGetters(["currentStats"]),
  },
};
</script>
