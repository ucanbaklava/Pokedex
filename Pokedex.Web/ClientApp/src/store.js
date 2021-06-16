import { createStore } from 'vuex'

import PokedexService from './services/PokedexService'


export const store = createStore({
    state: {
        currentPokemon: null,
        currentAbility: null,
        currentStats: null,
        currentPokemonFlavorText: null,
        currentGender: null,
        searchResults: null,
        detailedSearchResults: [],
        evolutionTree: null,
        pokemonListByType: null,
        pokemonMovesByLevel: null,
        pokemonByMove: null,
        moveDetail: null,
        allMoves: []

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
        SET_CURRENT_STATS(state, stats) {
            state.currentStats = stats
        },        
        SET_POKEMON_LIST_BY_TYPE(state, data) {
            state.pokemonListByType = data
        },        
        SET_CURRENT_POKEMON_FLAVOR_TEXT(state, data) {
            state.currentPokemonFlavorText = data
        },
        SET_CURRENT_GENDER(state, data) {
            state.currentGender = data
        },
        SET_DETAILED_SEARCH_RESULTS(state, data) {
            state.detailedSearchResults = data
        },
        SET_POKEMON_MOVES_BY_LEVEL(state, data) {
            state.pokemonMovesByLevel = data
        },
        SET_POKEMON_BY_MOVE(state, data) {
            state.pokemonByMove = data
        },
        SET_MOVE_DETAIL(state, data) {
            state.moveDetail = data
        },
        SET_ALL_MOVES(state, data) {
            state.allMoves = data
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
        currentPokemonId(state) {
            return state.currentPokemon.id
        },   
        currentGender(state) {
            return state.currentGender
        },      
        currentStats(state) {
            return state.currentStats
        },
        currentPokemonFlavorText(state) {
            return state.currentPokemonFlavorText
        },        
        searchResults(state) {
            return state.searchResults
        },
        detailedSearchResults(state) {
            return state.detailedSearchResults
        },
        evolutionTree(state) {
            return state.evolutionTree
        },
        pokemonListByType(state) {
            return state.pokemonListByType
        },
        moveDetail(state) {
            return state.moveDetail
        },
        pokemonMovesByLevel(state) {
            return state.pokemonMovesByLevel
        },
        pokemonByMove(state) {
            return state.pokemonByMove
        },
        allMoves(state) {
            return state.allMoves
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
            commit('SET_SEARCH_RESULTS', null)

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
        getGenderByIdentifier({commit}, identifier) {
            commit('SET_CURRENT_GENDER', null)
            return PokedexService.getGenderByIdentifier(identifier).then(result => {
                commit('SET_CURRENT_GENDER', result.data)
            })
        },        
        getStatsByIdentifier({commit}, identifier) {
            commit('SET_CURRENT_STATS', null)
            return PokedexService.getStatsByIdentifier(identifier).then(result => {
                commit('SET_CURRENT_STATS', result.data)
            })
        },     
        getFlavorTextByIdentifier({commit}, identifier) {
            commit('SET_CURRENT_POKEMON_FLAVOR_TEXT', null)
            return PokedexService.getFlavorTextByIdentifier(identifier).then(result => {
                commit('SET_CURRENT_POKEMON_FLAVOR_TEXT', result.data)
            })
        },           
        getEvolutionTreeByIdentifier( {commit}, name) {
            commit('SET_EVOLUTION_TREE', null)
            return PokedexService.getEvolutionTreeByIdentifier(name).then(result => 
            {
                commit('SET_EVOLUTION_TREE', result.data)
            })
        },     
        getMovesByLevel({commit}, identifier) {
            commit('SET_POKEMON_MOVES_BY_LEVEL', null)
            return PokedexService.getMovesByLevel(identifier).then(result => {
                commit('SET_POKEMON_MOVES_BY_LEVEL', result.data)
            })
        },           
        getPokemonByMove({commit}, identifier) {
            commit('SET_POKEMON_BY_MOVE', null)
            return PokedexService.getPokemonByMove(identifier).then(result => {
                commit('SET_POKEMON_BY_MOVE', result.data)
            })
        },       
        getMoveDetail({commit}, identifier) {
            commit('SET_MOVE_DETAIL', null)
            return PokedexService.getMoveDetailByIdentifier(identifier).then(result => {
                commit('SET_MOVE_DETAIL', result.data)
            })
        },   
        getAllMoves({commit}) {
            return PokedexService.getAllMoves().then(result => {
                commit('SET_ALL_MOVES', result.data)
            })        
        }, 
        searchPokemon({commit}, identifier) {
            commit('SET_SEARCH_RESULTS', null)
            PokedexService.searchPokemonByIdentifier(identifier).then(result => {
                commit('SET_SEARCH_RESULTS', result.data)
            })
        },
        searchPokemonDetailed({commit}, identifier) {
            commit('SET_DETAILED_SEARCH_RESULTS', [])
            PokedexService.searchPokemonDetailed(identifier).then(result => {
                commit('SET_DETAILED_SEARCH_RESULTS', result.data)
            })
        }        
    }
})