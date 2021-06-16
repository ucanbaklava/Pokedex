using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IPokemonRepository Pokemons { get; }
        IMoveRepository Moves { get; }
    }
}
