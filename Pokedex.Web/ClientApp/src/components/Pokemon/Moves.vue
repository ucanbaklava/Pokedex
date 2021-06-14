<template>
<div v-if="pokemonMovesByLevel">
    <move-summary class="m-auto" />

    <div class="bg-white shadow-md rounded my-6" v-if="pokemonMovesByLevel.filter(x => x.pokemon_move_method_id == type).length > 0 ">
        <slot name="moves-title"></slot>
        <table class="text-left w-full border-collapse">
            <!--Border collapse doesn't work on this site yet but it's available in newer tailwind versions -->
            <thead>
                <tr>
                    <th class="py-4 px-6 bg-grey-lightest font-bold uppercase text-sm text-grey-dark border-b border-grey-light" v-if="type == 1 ">Level</th>
                    <th class="py-4 px-6 bg-grey-lightest font-bold uppercase text-sm text-grey-dark border-b border-grey-light">Name</th>
                    <th class="py-4 px-6 bg-grey-lightest font-bold uppercase text-sm text-grey-dark border-b border-grey-light">Type</th>

                    <th class="py-4 px-6 bg-grey-lightest font-bold uppercase text-sm text-grey-dark border-b border-grey-light">Category</th>
                    <th class="py-4 px-6 bg-grey-lightest font-bold uppercase text-sm text-grey-dark border-b border-grey-light">Power</th>
                    <th class="py-4 px-6 bg-grey-lightest font-bold uppercase text-sm text-grey-dark border-b border-grey-light">Accuracy</th>
                    <th class="py-4 px-6 bg-grey-lightest font-bold uppercase text-sm text-grey-dark border-b border-grey-light">PP</th>
                    <th class="py-4 px-6 bg-grey-lightest font-bold uppercase text-sm text-grey-dark border-b border-grey-light">Effect</th>

                </tr>
            </thead>
            <tbody>
                <tr class="hover:bg-grey-lighter" v-for="(move, index) in pokemonMovesByLevel.filter(x => x.pokemon_move_method_id == type)" :key="index">
                    <td class="py-4 px-6 border-b border-grey-light"  v-if="type == 1">{{move.level}}</td>
                    <td class="py-4 px-6 border-b border-grey-light"><router-link class="pokemon-link" :to="{name: 'PokemonByMove', params:{identifier: move.ability_identifier}}"> {{normalize(move.ability_identifier)}}</router-link></td>
                    <td class="py-4 px-6 border-b border-grey-light"><pokemon-type-tag :type="move.ability_type" /></td>

                    <td class="py-4 px-6 border-b border-grey-light">{{normalize(move.move_category)}}</td>
                    <td class="py-4 px-6 border-b border-grey-light">{{move.power}}</td>
                    <td class="py-4 px-6 border-b border-grey-light">{{move.accuracy}}</td>
                    <td class="py-4 px-6 border-b border-grey-light">{{move.pp}}</td>
                    <td class="py-4 px-6 border-b border-grey-light text-xs">{{move.flavor_text}}</td>

                </tr>

            </tbody>
        </table>
    </div>
</div>    
</template>

<script>
import {mapGetters} from 'vuex'
import PokemonTypeTag from './PokemonTypeTag.vue'
import stringFilters from '../../filters/stringFilters'
import MoveSummary from './MoveSummary.vue'
export default {
  components: { PokemonTypeTag, MoveSummary },
    computed: {
        ...mapGetters(['pokemonMovesByLevel'])
    },
    props:['type'],
    methods: {
        normalize(text) {
            return stringFilters.normalizeText(text)
        }
    }
    
}
</script>