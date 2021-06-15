<template>
  <header class="lg:px-16 px-6 bg-white flex flex-wrap items-center lg:py-0 py-2 bg-yellow-100">
    <div class="flex-1 flex justify-between items-center">
      <router-link :to="{name: 'Home'}"  class="w-10">
        <img src="@/assets/pokeball.svg" />
      </router-link>
  </div>

   <label  @click="showMenu = !showMenu" for="menu-toggle" class="cursor-pointer" >
      <svg class="fill-current text-gray-900" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 20 20"><title>menu</title><path d="M0 3h20v2H0V3zm0 6h20v2H0V9zm0 6h20v2H0v-2z"></path></svg>
   </label>
  <input class="hidden" type="checkbox" id="menu-toggle" />

  <div class=" lg:flex lg:items-center lg:w-auto w-full" id="menu" :class="showMenu ? 'display:block': 'hidden'">
    <nav>
      <ul class="lg:flex items-center justify-between text-base text-gray-700 pt-4 lg:pt-0">
        <li><a class="lg:p-4 py-3 px-0 block border-b-2 border-transparent hover:border-indigo-400" href="#">Pokémon List</a></li>
        <li><a class="lg:p-4 py-3 px-0 block border-b-2 border-transparent hover:border-indigo-400" href="#">Abilities</a></li>
        <li><a class="lg:p-4 py-3 px-0 block border-b-2 border-transparent hover:border-indigo-400" href="#">Types</a></li>
        <li><router-link :to="{name: 'AllMoves'}" class="lg:p-4 py-3 px-0 block border-b-2 border-transparent hover:border-indigo-400 lg:mb-0 mb-2">Moves</router-link></li>
        <li>
            <the-search-bar />
        </li>

      </ul>
    </nav>
  </div>
  </header>
</template>

<script>
import TheSearchBar from './TheSearchBar.vue';
export default {
  components: { TheSearchBar },
  name: "TheNavbar",
  data() {
    return {
      showMenu: false
    }
  },
  methods: {
    toggleNavbar: function(){
      this.showMenu = !this.showMenu;
    }
  }
}
</script>