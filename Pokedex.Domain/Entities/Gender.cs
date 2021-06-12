using Pokedex.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    public class Gender: BaseEntity
    {
        public List<string> identifier { get; set; } = new List<string>();
        public int hatch_counter { get; set; }
        public string gender_type { get; set; }
    }
}
