using Pokedex.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    public class Ability: BaseEntity
    {
        public string identifier { get; set; }
        public int generation_id { get; set; }
        public bool is_main_series { get; set; }
        public string flavor_text { get; set; }
        public bool is_hidden { get; set; }

    }
}
