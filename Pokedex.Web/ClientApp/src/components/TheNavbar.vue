<template>
  <header class="lg:px-16 px-6 bg-white flex flex-wrap items-center lg:py-0 py-2 bg-yellow-100">
    <div class="flex-1 flex justify-between items-center">
      <a href="#" class="w-10">
        <img src="@/assets/pokeball.svg" />
    </a>
  </div>

   <label for="menu-toggle" class="pointer-cursor lg:hidden block"><svg class="fill-current text-gray-900" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 20 20"><title>menu</title><path d="M0 3h20v2H0V3zm0 6h20v2H0V9zm0 6h20v2H0v-2z"></path></svg></label>
  <input class="hidden" type="checkbox" id="menu-toggle" />

  <div class="hidden lg:flex lg:items-center lg:w-auto w-full" id="menu">
    <nav>
      <ul class="lg:flex items-center justify-between text-base text-gray-700 pt-4 lg:pt-0">
        <li><a class="lg:p-4 py-3 px-0 block border-b-2 border-transparent hover:border-indigo-400" href="#">Features</a></li>
        <li><a class="lg:p-4 py-3 px-0 block border-b-2 border-transparent hover:border-indigo-400" href="#">Pricing</a></li>
        <li><a class="lg:p-4 py-3 px-0 block border-b-2 border-transparent hover:border-indigo-400" href="#">Documentation</a></li>
        <li><a class="lg:p-4 py-3 px-0 block border-b-2 border-transparent hover:border-indigo-400 lg:mb-0 mb-2" href="#">Support</a></li>
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