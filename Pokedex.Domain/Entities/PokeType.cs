using Dapper.Contrib.Extensions;
using Pokedex.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Entities
{
    [Table("types")]
    public class PokeType: BaseEntity
    {
        public string identifier { get; set; }
        public int generation_id { get; set; }
        public int damage_class_id { get; set; }

    }
}
