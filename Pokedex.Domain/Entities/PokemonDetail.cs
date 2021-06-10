using Pokedex.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    public class PokemonDetail: BaseEntity
    {
        public string Identifier { get; set; }
        public string Species { get; set; }
    }
}
