<template>
    <span class="main-title">Pokémon with <i>{{normalize($route.params.identifier)}}</i></span>
    <div class="flex flex-col w-1/2 p-3" v-if="currentAbility">
        <div class="overflow-x-auto sm:-mx-6 lg:-mx-8">
            <div class="py-2 align-middle inline-block min-w-full sm:px-6 lg:px-8">
            <div class="shadow overflow-hidden border-b border-gray-200 sm:rounded-lg  h-1/3">
                <table class="min-w-full divide-y divide-gray-200">
                <thead class="bg-gray-50">
                    <tr>
                    <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        #
                    </th>
                    <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        Name
                    </th>
                    <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        1 <sup>st</sup> ability
                    </th>                
                    <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        2 <sup>nd</sup> ability
                    </th>
                    <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        Hidden Ability
                    </th>
                    </tr>
                </thead>
                <tbody class="bg-white divide-y divide-gray-200">
                    <tr v-for="(ability, index) in currentAbility" :key="index">
                    <td class="px-6 py-4 whitespace-nowrap">
                        <div class="flex items-center">
                        <div class="flex-shrink-0">
                            <img class="rounded-full" :src="`https://img.pokemondb.net/sprites/sword-shield/icon/${ability.identifier}.png`">
                        </div>
                        <div class="ml-4">
                            <div class="text-sm font-medium text-gray-900">
                            </div>
                            <div class="text-sm text-gray-500">
                                {{ability.id}}
                            </div>
                        </div>
                        </div>
                    </td>
                    <td class="px-6 py-4 whitespace-nowrap">
                        <div class="text-sm text-gray-900"> 
                            <router-link class="pokemon-link" :to="{'name': 'Pokemon', params: {identifier: ability.identifier  }}"  >{{normalize(ability.identifier)}}</router-link>
                        </div>
                            <router-link v-for="(type, index) in ability.pokeTypes" :key="index"  :to="{'name': 'PokemonList', params: {identifier: type.identifier  }}" :class="['type-list-' + type.identifier]" class="type-icon leading-3">
                                {{ normalize(type.identifier) }}
                            </router-link>
                      </td>
                    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        <div class="text-sm text-gray-900" v-if="!ability.abilities[0].is_hidden" ><router-link href="" :to="{name: 'SingleAbility', params: { identifier: ability.abilities[0].identifier }}"  >{{normalize(ability.abilities[0].identifier)}}</router-link></div>
                        <div class="text-sm text-gray-900" v-else >-</div>
                    </td>
                    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500" v-if="ability.abilities.length  > 1">
                        <div class="text-sm text-gray-900" v-if="!ability.abilities[1].is_hidden" ><router-link href="" :to="{name: 'SingleAbility', params: { identifier: ability.abilities[1].identifier }}"  >{{normalize(ability.abilities[1].identifier)}}</router-link></div>
                        <div class="text-sm text-gray-900" v-else >-</div>

                    </td>
                    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        <div class="text-sm text-gray-900" v-if="ability.abilities.filter(x => x.is_hidden === true)[0]">
                            <router-link href="" :to="{name: 'SingleAbility', params: { identifier: ability.abilities.filter(x => x.is_hidden === true)[0].identifier }}">{{normalize(ability.abilities.filter(x => x.is_hidden === true)[0].identifier)}}</router-link>
                        </div>
                            <div class="text-sm text-gray-900" v-else >-</div>

                    </td>                
                    </tr>
                </tbody>
                </table>
            </div>
            </div>
        </div>
    </div>    
</template>

<script>
import {store} from '../../store'
import {mapGetters} from 'vuex'
import stringFilters from '../../filters/stringFilters'
export default {
    computed: {
        ...mapGetters(['currentAbility'])
    },
    methods: {
        normalize(text) {
            return stringFilters.normalizeText(text)
        }
    },
    beforeRouteEnter(to, from, next) {
        store.dispatch('getAbilityByIdentifier', to.params.identifier).then(() => next())
    },
    beforeRouteUpdate(to, from, next) {
        store.dispatch('getAbilityByIdentifier', to.params.identifier).then(() => next())
    }    
}
</script>