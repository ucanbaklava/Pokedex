using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    public class Evolution
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public int evolves_from_species_id { get; set; }
        public ICollection<PokeType> PokeType { get; set; } = new List<PokeType>();
    }
}
