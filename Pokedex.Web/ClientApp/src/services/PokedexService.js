import https from "../https-common"

class PokedexService {
    getById(id) {
        return https.get(`/pokemon/${id}`)
    }
    async getByIdentifier(identifier) {
        return await https.get(`/pokemon/${identifier}`)
    }
    getEvolutionTreeByIdentifier(identifier) {
        return https.get(`/pokemon/evolution/${identifier}`)
    }
    getPokemonListByType(identifier) {
        return https.get(`/pokemon/list/${identifier}`)
    }
    getAbilityByIdentifier(identifier) {
        return https.get(`/pokemon/ability/${identifier}`)
    }
    getPokemonByName(name) {
        return https.get(`/pokemon/${name}`)
    }
    searchPokemonByIdentifier(identifier) {
        return https.get(`/pokemon/search/${identifier}`)
    }
}

export default new PokedexService()