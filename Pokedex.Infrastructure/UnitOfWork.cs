using Pokedex.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        public IPokemonRepository Pokemons { get;}
        public UnitOfWork(IPokemonRepository pokemonRepository)
        {
            Pokemons = pokemonRepository;
        }
    }
}
