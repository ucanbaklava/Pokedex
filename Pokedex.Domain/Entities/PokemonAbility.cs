using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    [Table("pokemon-ability")]
    public class PokemonAbility
    {
        public int pokemon_id { get; set; }
        public int ability_id { get; set; }
    }
}
