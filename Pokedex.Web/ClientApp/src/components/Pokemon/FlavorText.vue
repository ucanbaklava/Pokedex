<template>
    <div class="poke-data-box">
        <span class="flavor_text_title">Flavor text from <i>Pokémon {{normalize(currentPokemonFlavorText.identifier)}}:</i></span>
        {{currentPokemonFlavorText.flavor_text}}
    </div>
</template>

<script>
import {mapGetters} from 'vuex'
import stringFilters from '../../filters/stringFilters'
export default {
    computed: {
        ...mapGetters(['currentPokemonFlavorText'])
    },
    methods: {
        normalize(text) {
            return stringFilters.normalizeText(text)
        }
    }
    
}
</script>