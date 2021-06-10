import { createStore } from 'vuex'

import PokedexService from './services/PokedexService'


export const store = createStore({
    state: {
        currentPokemon: null,
        currentAbility: null,
        searchResults: null,
        evolutionTree: null,
        pokemonListByType: null
    },
    mutations: {
        SET_CURRENT_POKEMON(state, pokemon) {
            state.currentPokemon = pokemon
        },
        SET_CURRENT_ABILITY(state, ability) {
            state.currentAbility = ability
        },
        SET_SEARCH_RESULTS(state, searchResult) {
            state.searchResults = searchResult
        },
        SET_EVOLUTION_TREE(state, data) {
            state.evolutionTree = data
        },
        SET_POKEMON_LIST_BY_TYPE(state, data) {
            state.pokemonListByType = data
        }        
    },
    getters: {
        currentPokemon(state) {
            return state.currentPokemon
        },
        currentAbility(state) {
            return state.currentAbility
        },        
        currentPokemonName(state) {
            return state.currentPokemon.identifier
        },
        searchResults(state) {
            return state.searchResults
        },
        evolutionTree(state) {
            return state.evolutionTree
        },
        pokemonListByType(state) {
            return state.pokemonListByType
        },
        is_pokemon_loaded (state) {
            return !!state.currentPokemon
        }        
    },
    actions: {
        getById({commit}, id) {
            PokedexService.getById(id).then(result => 
            {
                commit('SET_CURRENT_POKEMON', result.data)
            })
        },
        async getPokemonByIdentifier( {commit}, name) {
            commit('SET_CURRENT_POKEMON', null)
            console.log("geldim")
            return await PokedexService.getByIdentifier(name).then(result => 
            {
                console.log(result.data)
                commit('SET_CURRENT_POKEMON', result.data)
            })
        },
        getPokemonListByType({commit}, identifier) {
            commit('SET_POKEMON_LIST_BY_TYPE', null)
            return PokedexService.getPokemonListByType(identifier).then(result => {
                commit('SET_POKEMON_LIST_BY_TYPE', result.data)
            })
        },
        getAbilityByIdentifier({commit}, identifier) {
            commit('SET_CURRENT_ABILITY', null)
            return PokedexService.getAbilityByIdentifier(identifier).then(result => {
                commit('SET_CURRENT_ABILITY', result.data)
            })
        },
        getEvolutionTreeByIdentifier( {commit}, name) {
            commit('SET_EVOLUTION_TREE', null)
            return PokedexService.getEvolutionTreeByIdentifier(name).then(result => 
            {
                commit('SET_EVOLUTION_TREE', result.data)
            })
        },                  
        searchPokemon({commit}, identifier) {
            if(!identifier)  commit('SET_SEARCH_RESULTS', null)
            PokedexService.searchPokemonByIdentifier(identifier).then(result => {
                commit('SET_SEARCH_RESULTS', result.data)
            })
        }
    }
})