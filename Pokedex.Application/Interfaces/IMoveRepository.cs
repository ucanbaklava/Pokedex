using Pokedex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Application.Interfaces
{
    public interface IMoveRepository: IGenericRepository<PokemonMove>
    {
    }
}
