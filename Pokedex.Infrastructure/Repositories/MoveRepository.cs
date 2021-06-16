using Dapper;
using Microsoft.Extensions.Configuration;
using Pokedex.Application.Interfaces;
using Pokedex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Infrastructure.Repositories
{
    public class MoveRepository : IMoveRepository
    {
        private readonly IConfiguration configuration;
        public MoveRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IReadOnlyList<PokemonMove>> GetAllAsync()
        {
            var sql = @"select m.id as move_id, m.identifier, m.power, m.accuracy, m.pp, mft.flavor_text, t.identifier as type_identifier, mdc.identifier as category_identifier, mep.short_effect from moves m 
                        join move_damage_classes mdc on mdc.id = m.damage_class_id 
                        join types t on t.id = m.type_id 
                        join move_effect_prose mep on mep.move_effect_id = m.effect_id 
                        join move_flavor_text mft ON mft.move_id = m.id 
                        where mft.language_id = 9 and mft.version_group_id = 20
                        order by identifier ";
            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PokemonMove>(sql);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<PokemonMove>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var parameters = new { PageNumber = pageNumber, PageSize = pageSize };
            var sql = @"select m.id as move_id, m.identifier, m.power, m.accuracy, m.pp, mft.flavor_text, t.identifier as type_identifier, mdc.identifier as category_identifier, mep.short_effect from moves m 
                        join move_damage_classes mdc on mdc.id = m.damage_class_id 
                        join types t on t.id = m.type_id 
                        join move_effect_prose mep on mep.move_effect_id = m.effect_id 
                        join move_flavor_text mft ON mft.move_id = m.id 
                        where mft.language_id = 9 and mft.version_group_id = 20
                        order by identifier 
                        limit @PageSize offset (@PageNumber -1) * @PageSize";
            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PokemonMove>(sql, param: parameters);
                return result.ToList();
            }
        }

        public Task<PokemonMove> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
