using Pokedex.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    public class PokemonStat: BaseEntity
    {
        public string identifier { get; set; }
        public int base_stat { get; set; }
    }
}
