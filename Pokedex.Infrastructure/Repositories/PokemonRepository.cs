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
            var sql = @" with pokemons as(
                     select ps.id from pokemon_species ps 
                     join pokemon_abilities pa  on pa.pokemon_id = ps.id 
                     join abilities a on a.id = pa.ability_id
                     where a.identifier = @Identifier
                     ), abilities as (
	                    select ps2.id, ps2.identifier, a2.id, a2.identifier, pa2.is_hidden from pokemon_species ps2 
	                    join pokemon_abilities pa2 on pa2.pokemon_id = ps2.id 
	                    join abilities a2 on a2.id = pa2.ability_id 
	                    where ps2.id in (select id from pokemons)
                    	order by ps2.id
                     ) select * from abilities";

            var pokeDictionary = new Dictionary<int, Pokemon>();

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Pokemon, Ability, Pokemon>(
                    sql,
                    (p, a) =>
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

                        return pokeEntry;
                    }, parameters, splitOn: "id,id");


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
            var sql = @"select p.*, ab.*, types.* from pokemon p 
                inner join pokemon_abilities pa on p.id = pa.pokemon_id 
                inner join abilities ab on pa.ability_id = ab.id
                inner join pokemon_types ON pokemon_types.pokemon_id = p.id
                inner join types on types.id = pokemon_types.type_id
                where p.id = @id
                ";

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
            var sql = @"select  p.*, fat.flavor_text, ab.*, types.* from pokemon p 
                inner join pokemon_abilities pa on p.id = pa.pokemon_id 
                inner join abilities ab on pa.ability_id = ab.id
                inner join pokemon_types ON pokemon_types.pokemon_id = p.id
                inner join types on types.id = pokemon_types.type_id
				left join (
					SELECT distinct ability_id,flavor_text, version_group_id FROM ability_flavor_text 
					where language_id= 9
					order by  version_group_id desc   limit 1
				) fat on fat.ability_id = ab.id
                where p.identifier = @Identifier
                ";

            var pokeDictionary = new Dictionary<int, Pokemon>();

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Pokemon, string, Ability, PokeType, Pokemon>(
                    sql,
                    (p, f, a, t) =>
                    {
                        Pokemon pokeEntry;
                        if (!pokeDictionary.TryGetValue(p.id, out pokeEntry))
                        {
                            pokeEntry = p;
                            pokeDictionary.Add(p.id, pokeEntry);
                        }

                        if (!pokeEntry.Abilities.Any(x => x.id == a.id))
                        {
                            a.flavor_text = f;
                            pokeEntry.Abilities.Add(a);
                        }

                        if (!pokeEntry.PokeTypes.Any(x => x.id == t.id))
                        {
                            pokeEntry.PokeTypes.Add(t);
                        }

                        return pokeEntry;
                    }, parameters, splitOn: "id, flavor_text, id, id");


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
	                    select ps.id, ps.identifier, ps.evolves_from_species_id from pokemon_species ps
	                    where ps.identifier = @Identifier
		                    union all
	                    select ps2.id, ps2.identifier, ps2.evolves_from_species_id from pokemon_species ps2
	                    join parent on parent.id = ps2.evolves_from_species_id 
                    ), tree as (
	                    select ps.id, ps.identifier, ps.evolves_from_species_id, types.id, types.identifier from pokemon_species ps
	                    join pokemon_types on pokemon_types.pokemon_id = ps.id 
	                    join types on types.id = pokemon_types.type_id 
	                    where ps.id = (select max(id) from parent)
	                    union all 
	                    select ps.id, ps.identifier, ps.evolves_from_species_id, types.id, types.identifier from pokemon_species ps
	                    join pokemon_types on pokemon_types.pokemon_id = ps.id 	
	                    join types on types.id = pokemon_types.type_id 
	
	                    join tree on tree.evolves_from_species_id = ps.id
                    ) select distinct * from tree;";

            using (var connection = new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Evolution, PokeType, Evolution>(sql,
                    (e, pt) => 
                    {
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


/*
 * 

WITH RECURSIVE cte AS (
	SELECT pokemon_species.id,identifier, evolves_from_species_id FROM pokemon_species
	left join pokemon_evolution on pokemon_evolution.id = pokemon_species.evolves_from_species_id
	WHERE
		pokemon_species.id = 40
	UNION
		SELECT p.id,p.identifier, p.evolves_from_species_id FROM pokemon_species p
		left join pokemon_evolution on pokemon_evolution.evolved_species_id = p.id
		INNER JOIN cte  ON cte.id= p.evolves_from_species_id
	
) SELECT
	*
FROM
	cte
	union
		SELECT pokemon_species.id,identifier, evolves_from_species_id FROM pokemon_species
	inner join pokemon_evolution on pokemon_species.evolves_from_species_id = ANY(select id from cte)

	
*/

/*
 * 
with recursive deneme as (
	select pokemon_species.id, identifier, evolves_from_species_id from pokemon_species where id=439
	union all
	select p.id, p.identifier, p.evolves_from_species_id from pokemon_species p
	INNER JOIN deneme  ON deneme.id= p.evolves_from_species_id
) , poketree as (
		SELECT pokemon_species.id,identifier, evolves_from_species_id FROM pokemon_species
		where pokemon_species.id = (select max(id) from deneme)
	union all
		SELECT pokemon_species.id, pokemon_species.identifier, pokemon_species.evolves_from_species_id FROM pokemon_species
		inner join poketree on poketree.evolves_from_species_id  = pokemon_species.id

) select * from poketree
*/