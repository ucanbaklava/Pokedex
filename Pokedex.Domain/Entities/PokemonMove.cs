using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    public class PokemonMove
    {
        public int move_id { get; set; }
        public int pokemon_move_method_id { get; set; }
        public int level { get; set; }
        public string identifier { get; set; }
        public string ability_identifier { get; set; }
        public string ability_type { get; set; }
        public string move_category { get; set; }
        public int power { get; set; }
        public int accuracy { get; set; }
        public int pp { get; set; }
        public string flavor_text { get; set; }
        public string type_identifier { get; set; }
        public string category_identifier { get; set; }
    }
}
