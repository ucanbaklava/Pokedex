using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    [Table("ability_flavor_text")]
    public class AbilityFlavorText
    {
        public string flavor_text { get; set; }
    }
}
