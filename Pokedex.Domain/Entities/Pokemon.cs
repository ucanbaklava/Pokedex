using Pokedex.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    public class Pokemon: BaseEntity
    {
        public string identifier { get; set; }
        //public int species_id { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public int base_expreince { get; set; }
        public int order { get; set; }
        public bool is_default { get; set; }
        public string habitat { get; set; }
        public ICollection<Ability> Abilities { get; set; } = new List<Ability>();
        public ICollection<PokeType> PokeTypes { get; set; } = new List<PokeType>();
    }
}
