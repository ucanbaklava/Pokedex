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
    public class PokemonRepository: IPokemonRepository
    {
        private readonly IConfiguration configuration;
        public PokemonRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<Pokemon>> GetAbilityByIdentifier(string identifier)
        {
            var parameters = new { Identifier = identifier };
            var sql = @"  with pokemons as(
                     select ps.id from pokemon_species ps 
                     join pokemon_abilities pa  on pa.pokemon_id = ps.id 
                     join abilities a on a.id = pa.ability_id
                     where a.identifier = @Identifier
                     ), abilities as (
	                    select ps2.id, ps2.identifier, t.id, t.identifier, a2.id, a2.identifier, pa2.is_hidden from pokemon_species ps2 
	                    join pokemon_abilities pa2 on pa2.pokemon_id = ps2.id 
	                    join pokemon_types pt on pt.pokemon_id = ps2.id 
	                    join types t on t.id = pt.type_id 
	                    join abilities a2 on a2.id = pa2.ability_id 
	                    where ps2.id in (select id from pokemons)
                    	order by ps2.id
                     ) select * from abilities ";

            var pokeDictionary = new Dictionary<int, Pokemon>();

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Pokemon, PokeType, Ability, Pokemon>(
                    sql,
                    (p, t, a) =>
                    {
                        Pokemon pokeEntry;
                        if (!pokeDictionary.TryGetValue(p.id, out pokeEntry))
                        {
                            pokeEntry = p;
                            pokeDictionary.Add(p.id, pokeEntry);
                        }

                        if (!pokeEntry.Abilities.Any(x => x.id == a.id))
                        {
                            pokeEntry.Abilities.Add(a);
                        }

                        if (!pokeEntry.PokeTypes.Any(x => x.id == t.id))
                        {
                            pokeEntry.PokeTypes.Add(t);
                        }

                        return pokeEntry;
                    }, parameters, splitOn: "id,id,id");


                return result.Distinct().ToList();
            }
        }

        public async Task<IReadOnlyList<Pokemon>> GetAllAsync()
        {
            var sql = "SELECT * FROM Pokemon";
            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Pokemon>(sql);
                return result.ToList();
            }
        }

        public async Task<Pokemon> GetByIdAsync(int id)
        {
            var parameters = new { id = id};
            /*var sql = @"select p.*, ab.*, types.* from pokemon p 
                inner join pokemon_abilities pa on p.id = pa.pokemon_id 
                inner join abilities ab on pa.ability_id = ab.id
                inner join pokemon_types ON pokemon_types.pokemon_id = p.id
                inner join types on types.id = pokemon_types.type_id
                where p.id = @id
                ";*/
            var sql = @"select ps.id, ps.identifier, p.height, p.weight, p.base_experience, p.order, ph.identifier as habitat,  ab.*,pa.is_hidden,  types.* from pokemon_species ps 
                inner join pokemon_abilities pa on ps.id = pa.pokemon_id
                inner join abilities ab on pa.ability_id = ab.id
                inner join pokemon_types ON pokemon_types.pokemon_id = ps.id
                inner join types on types.id = pokemon_types.type_id
                join pokemon_habitats ph on ph.id = ps.habitat_id
                join pokemon p on p.id = ps.id
                where ps.id = 4 ";

            var pokeDictionary = new Dictionary<int, Pokemon>();

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Pokemon, Ability, PokeType, Pokemon>(
                    sql,
                    (p, a, t) =>
                    {
                        Pokemon pokeEntry;
                        if (!pokeDictionary.TryGetValue(p.id, out pokeEntry))
                        {
                            pokeEntry = p;
                            pokeDictionary.Add(p.id, pokeEntry);
                        }

                        if (!pokeEntry.Abilities.Any(x => x.id == a.id))
                        {
                            pokeEntry.Abilities.Add(a);
                        }

                        if (!pokeEntry.PokeTypes.Any(x => x.id == t.id))
                        {
                            pokeEntry.PokeTypes.Add(t);
                        }

                        return pokeEntry;
                    }, parameters, splitOn: "id, id, id");


                return result.First();
            }

        }

        public async Task<Pokemon> GetByIdentifier(string identifier)
        {
            var parameters = new { Identifier = identifier };

            var sql = @"with flavor_texts as(
	                select distinct on(aft.ability_id) aft.ability_id ,aft.flavor_text from ability_flavor_text aft 
	                where aft.language_id = 9
                ), pokemons as (                  
                   
                select ps.id, ps.identifier, p.height, p.weight, p.base_experience, p.order, ph.identifier as habitat,  ab.*, aft.flavor_text , pa.is_hidden,  types.* from pokemon_species ps 
                                inner join pokemon_abilities pa on ps.id = pa.pokemon_id 
                                inner join abilities ab on pa.ability_id = ab.id
                                inner join pokemon_types ON pokemon_types.pokemon_id = ps.id
                                inner join types on types.id = pokemon_types.type_id
                                join flavor_texts aft on aft.ability_id = pa.ability_id 
                                left join pokemon_habitats ph on ph.id = ps.habitat_id
                                join pokemon p on p.id = ps.id 
                                where ps.identifier = @Identifier
                ) select * from pokemons";

            var pokeDictionary = new Dictionary<int, Pokemon>();

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Pokemon, Ability, PokeType, Pokemon>(
                    sql,
                    (p, a, t) =>
                    {
                        Pokemon pokeEntry;
                        if (!pokeDictionary.TryGetValue(p.id, out pokeEntry))
                        {
                            pokeEntry = p;
                            pokeDictionary.Add(p.id, pokeEntry);
                        }

                        if (!pokeEntry.Abilities.Any(x => x.id == a.id))
                        {
                            pokeEntry.Abilities.Add(a);
                        }

                        if (!pokeEntry.PokeTypes.Any(x => x.id == t.id))
                        {
                            pokeEntry.PokeTypes.Add(t);
                        }

                        return pokeEntry;
                    }, parameters, splitOn: "id,id,id");


                return result.First();
            }
        }

        public async Task<List<Evolution>> GetEvolutionTreeByIdentifier(string identifier)
        {
            var parameters = new { Identifier = identifier };
            /*var sql = @"with recursive parent as (
	            select ps.id, ps.identifier, ps.evolves_from_species_id from pokemon_species ps
	            where ps.identifier = @Identifier
		            union all
	            select ps2.id, ps2.identifier, ps2.evolves_from_species_id from pokemon_species ps2
	            join parent on parent.id = ps2.evolves_from_species_id 
            ), tree as (
	            select ps.id, ps.identifier, ps.evolves_from_species_id from pokemon_species ps
	            where ps.id = (select max(id) from parent)
	            union all 
	            select ps.id, ps.identifier, ps.evolves_from_species_id from pokemon_species ps
	            join tree on tree.evolves_from_species_id = ps.id
            ) select * from tree";*/
            var evoDictionary = new Dictionary<int, Evolution>();

            var sql = @"with recursive parent as (
	                    select ps.id, ps.identifier, ps.evolves_from_species_id, pokemon.order   as poke_order from pokemon_species ps
	                    join pokemon on pokemon.id  = ps.id 	                    
	                    where ps.identifier = @Identifier
	                    
		                    union all
		                    
	                    select ps2.id, ps2.identifier, ps2.evolves_from_species_id, pokemon.order as poke_order from pokemon_species ps2
	                    join pokemon on pokemon.id  = ps2.id 	                  
	                    join parent on parent.id = ps2.evolves_from_species_id 
                    ), tree as (
	                    select ps.id, ps.identifier, ps.evolves_from_species_id, pe.minimum_level, et.identifier as evolution_type, i.identifier as item, p.order as poke_order, types.id, types.identifier from pokemon_species ps
	                    join pokemon_types on pokemon_types.pokemon_id = ps.id 
	                    join types on types.id = pokemon_types.type_id
	                    left join pokemon_evolution pe on pe.evolved_species_id = ps.id
	                    left join evolution_triggers et on et.id = pe.evolution_trigger_id 
						left join items i on i.id = pe.trigger_item_id
						join pokemon p on p.id = ps.id 

	                    where ps.id = (select id from parent order by poke_order desc limit 1)
	                    union all 
	                    select ps.id, ps.identifier, ps.evolves_from_species_id,  pe.minimum_level,   et2.identifier as evolution_type, i.identifier as item, p.order as poke_order, types.id, types.identifier from pokemon_species ps
	                    join pokemon_types on pokemon_types.pokemon_id = ps.id 
	                    left join pokemon_evolution pe on pe.evolved_species_id = ps.id 
	                    join types on types.id = pokemon_types.type_id 
	                    left join evolution_triggers et2 on et2.id = pe.evolution_trigger_id 
						left join items i on i.id = pe.trigger_item_id 
						join pokemon p on p.id = ps.id 
	                    join tree on tree.evolves_from_species_id = ps.id
                    ) select distinct * from tree order by poke_order;";

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Evolution, PokeType, Evolution>(sql,
                    (e, pt) => 
                    {
                        if (e == null)
                        {
                            return null;
                        }
                        Evolution pokeEvolution;
                        if (!evoDictionary.TryGetValue(e.id, out pokeEvolution))
                        {
                            pokeEvolution = e;
                            evoDictionary.Add(e.id, pokeEvolution);
                        }

                        if (!pokeEvolution.PokeType.Any(x => x.id == pt.id))
                        {
                            pokeEvolution.PokeType.Add(pt);                        
                        }
                        return pokeEvolution;

                    }, parameters, splitOn: "id,id");


                return result.Distinct().ToList();
            }

        }

        public async Task<PokemonFlavorText> GetPokemonFlavorText(string identifier)
        {
            var parameters = new { Identifier = identifier };


            var sql = @" select * from                    
                    (select distinct on(psft.flavor_text) flavor_text, v.identifier from pokemon_species_flavor_text psft  
                    join versions v on v.id = psft.version_id
                    join pokemon_species ps on ps.id = psft.species_id 
                    where  ps.identifier = @Identifier and psft.language_id = 9) flavor_texts
                    order by random() 
                    limit 1 ";
            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PokemonFlavorText>(sql, param: parameters);

                return result.FirstOrDefault();
            }
        }

        public async Task<Gender> GetPokemonGender(string identifier)
        {
            var parameters = new { Identifier = identifier };

            var sql = @"                   
                    select ps.id, ps.hatch_counter,  case
 	                    when ps.gender_rate = -1 then 'Genderless'
 	                    when ps.gender_rate = 0 then '100% Male, 0% Female'
 	                    when ps.gender_rate = 1 then '87,5% Male, 12,5% Female'
 	                    when ps.gender_rate = 2 then '75% Male, 25% Female'
 	                    when ps.gender_rate = 4 then '50% Male, 50% Female'
 	                    when ps.gender_rate = 6 then '25% Male, 75% Female'
 	                    when ps.gender_rate = 7 then '12,5% Male, 87,5% Female'
 	                    when ps.gender_rate = 8 then '0% Male, 100% Female'
                    end as gender_type, eg.identifier  from egg_groups eg 
                    join pokemon_egg_groups peg on peg.egg_group_id = eg.id 
                    join pokemon_species ps on ps.id = peg.species_id 
                    where ps.identifier  = @Identifier
                    ";
            var dict = new Dictionary<int, Gender>();

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Gender, string, Gender>(sql, (g,x) =>
                {
                    Gender gender;

                    if (!dict.TryGetValue(g.id, out gender))
                    {
                        gender = g;
                        gender.identifier.Add(x);
                        dict.Add(g.id, gender);
                    }

                    else if(dict.TryGetValue(g.id, out gender))
                    {
                        gender.identifier.Add(x);
                    }

                    return gender;
                }, parameters, splitOn:"id,identifier");

                return result.FirstOrDefault();
            }
        }

        public async Task<List<PokemonStat>> GetPokemonStats(string identifier)
        {
            var parameters = new { Identifier = identifier };


            var sql = @"select s.id , s.identifier, ps.base_stat from pokemon_stats ps 
                join stats s on s.id  = ps.stat_id 
                join pokemon_species ps2 on ps2.id = ps.pokemon_id 
                where ps2.identifier = @Identifier
                ";

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PokemonStat>(sql, param: parameters);

                return result.ToList();
            }
        }

        public async Task<List<Pokemon>> ListPokemonsByType(string identifier)
        {
            var parameters = new { Identifier = identifier };
            var pokeDictionary = new Dictionary<int, Pokemon>();

            var sql = @" with pokemons_by_type as(
                 select ps.id from pokemon_species ps  
                 join pokemon_types pt  on pt.pokemon_id = ps.id 
                 join types on types.id = pt.type_id 
                 where types .identifier = @identifier
                 ), pokes as(
                 select ps2.id, ps2.identifier, pt2.type_id as id, types.identifier from pokemon_types pt2 
                 join pokemon_species ps2 on ps2.id = pt2.pokemon_id 
                 join types on types.id = pt2.type_id 
                 where pt2.pokemon_id  in (select id from pokemons_by_type)
                 ) select * from pokes";

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Pokemon, PokeType, Pokemon>(sql,
                    (p, pt) =>
                    {
                        Pokemon pokemon;
                        if (!pokeDictionary.TryGetValue(p.id, out pokemon))
                        {
                            pokemon = p;
                            pokeDictionary.Add(p.id, pokemon);
                        }

                        if (!pokemon.PokeTypes.Any(x => x.id == pt.id))
                        {
                            pokemon.PokeTypes.Add(pt);
                        }
                        return pokemon;

                    }, parameters, splitOn: "id,id");


                return result.Distinct().ToList();
            }
        }

        public async Task<List<string>> SearchPokemonByIdentifier(string identifier)
        {

            var parameters = new { Identifier = identifier };
            var sql = @"select * from pokemon_search(@Identifier)";

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<string>(
                    sql, parameters);

                return result.ToList();
            }
        }
    }
}
