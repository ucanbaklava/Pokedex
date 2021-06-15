using Pokedex.Application.Interfaces;
using Pokedex.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        public IPokemonRepository Pokemons { get;}
        public IMoveRepository Moves { get; set; }
        public UnitOfWork(IPokemonRepository pokemonRepository, IMoveRepository moveRepository)
        {
            Pokemons = pokemonRepository;
            Moves = moveRepository;
        }
    }
}
