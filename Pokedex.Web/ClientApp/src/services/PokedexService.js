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
    getStatsByIdentifier(identifier) {
        return https.get(`/pokemon/stats/${identifier}`)
    }
    getGenderByIdentifier(identifier) {
        return https.get(`/pokemon/gender/${identifier}`)
    }
    getFlavorTextByIdentifier(identifier) {
        return https.get(`/pokemon/flavortext/${identifier}`)
    }    
    getPokemonByName(name) {
        return https.get(`/pokemon/${name}`)
    }
    getMovesByLevel(identifier) {
        return https.get(`/pokemon/moves_level/${identifier}`)
    }
    searchPokemonByIdentifier(identifier) {
        return https.get(`/pokemon/search/${identifier}`)
    }
    searchPokemonDetailed(identifier) {
        return https.get(`/pokemon/searchdetailed/${identifier}`)
    }
}

export default new PokedexService()