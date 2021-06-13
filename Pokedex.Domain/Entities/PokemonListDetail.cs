using Pokedex.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    public class PokemonListDetail: BaseEntity
    {
        public string identifier { get; set; }
        public List<PokeType> PokeTypes { get; set; } = new List<PokeType>();
        public List<PokemonStat> PokemonStats { get; set; } = new List<PokemonStat>();
        public List<Ability> Abilities { get; set; } = new List<Ability>();
    }

    /*public class Detail
    {
        public int move_id { get; set; }
        public int level { get; set; }
        public string ability_identifier { get; set; }
        public string ability_type { get; set; }
        public string move_category { get; set; }
        public int power { get; set; }
        public int accuracy { get; set; }
        public int pp { get; set; }
        public string flavor_text { get; set; }
    }*/
}
