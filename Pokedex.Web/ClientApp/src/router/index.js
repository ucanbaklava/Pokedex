import { createWebHistory, createRouter } from "vue-router";
import Home from "@/components/Home.vue";
import Pokemon from "@/components/Pokemon/Pokemon.vue"
import PokemonList from "@/components/Type/PokemonList.vue"
import SingleAbility from "@/components/Abilities/SingleAbility.vue"
import PokemonByMove from "@/components/Pokemon/PokemonByMove.vue"
const routes = [
    {
        path: "/",
        name: "Home",
        component: Home,
    },
    {
        path: "/pokemon/:identifier",
        name: "Pokemon",
        component: Pokemon,
        params: true,
    },
    {
        path: "/type/:identifier",
        name: "PokemonList",
        component: PokemonList,
        params: true,
    },
    {
        path: "/ability/:identifier",
        name: "SingleAbility",
        component: SingleAbility,
        params: true,
    },
    {
        path: "/move/:identifier",
        name: "PokemonByMove",
        component: PokemonByMove,
        params: true,
    }                 
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;