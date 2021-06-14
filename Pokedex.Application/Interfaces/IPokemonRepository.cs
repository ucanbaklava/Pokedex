using Pokedex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Application.Interfaces
{
    public interface IPokemonRepository : IGenericRepository<Pokemon>
    {
        Task<Pokemon> GetByIdentifier(string identifier);
        Task<List<string>> SearchPokemonByIdentifier(string identifier);
        Task<List<Evolution>> GetEvolutionTreeByIdentifier(string identifier);
        Task<List<Pokemon>> ListPokemonsByType(string identifier);
        Task<List<Pokemon>> GetAbilityByIdentifier(string identifier);
        Task<List<PokemonStat>> GetPokemonStats(string identifier);
        Task<PokemonFlavorText> GetPokemonFlavorText(string identifier);
        Task<Gender> GetPokemonGender(string identifier);
        Task<List<PokemonListDetail>> SearchPokemonDetailed(string identifier);
        Task<List<PokemonMove>> GetPokemonMovesLevel(string identifier);
        Task<List<Pokemon>> GetPokemonsByMove(string identifier);
        Task<PokemonMove> GetMoveDetail(string identifier);

    }
}
