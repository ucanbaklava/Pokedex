using Pokedex.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    public class Stat: BaseEntity
    {
        public int HP { get; set; }
        public int Attack { get; set; } 
        public int SpecialAttack { get; set; }
        public int Defence { get; set; }
        public int SpecialDefence { get; set; }
        public int Speed { get; set; }
    }
}
