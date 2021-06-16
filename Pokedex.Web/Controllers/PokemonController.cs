using Microsoft.AspNetCore.Mvc;
using Pokedex.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public PokemonController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Pokemons.GetAllAsync();
            return Ok(data);
        }
        /*
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Pokemons.GetByIdAsync(id);
            return Ok(data);
        }*/

        [HttpGet("{identifier}")]
        public async Task<IActionResult> GetByIdentifier(string identifier)
        {
            var data = await unitOfWork.Pokemons.GetByIdentifier(identifier);
            return Ok(data);
        }

        [HttpGet("search/{identifier}")]
        public async Task<IActionResult> SearchPokemonByIdentifier(string identifier)
        {
            var data = await unitOfWork.Pokemons.SearchPokemonByIdentifier(identifier);
            return Ok(data);
        }

        [HttpGet("evolution/{identifier}")]
        public async Task<IActionResult> GetPokemonEvolutionTree(string identifier)
        {
            var data = await unitOfWork.Pokemons.GetEvolutionTreeByIdentifier(identifier);
            return Ok(data);
        }

        [HttpGet("list/{identifier}")]
        public async Task<IActionResult> GetPokemonListByType(string identifier)
        {
            var data = await unitOfWork.Pokemons.ListPokemonsByType(identifier);
            return Ok(data);
        }

        [HttpGet("ability/{identifier}")]
        public async Task<IActionResult> GetAbilityByIdentifier(string identifier)
        {
            var data = await unitOfWork.Pokemons.GetAbilityByIdentifier(identifier);
            return Ok(data);
        }

        [HttpGet("stats/{identifier}")]
        public async Task<IActionResult> GetPokemonStatsByIdentifier(string identifier)
        {
            var data = await unitOfWork.Pokemons.GetPokemonStats(identifier);
            return Ok(data);
        }
        [HttpGet("flavortext/{identifier}")]
        public async Task<IActionResult> GetPokemonFlavorText(string identifier)
        {
            var data = await unitOfWork.Pokemons.GetPokemonFlavorText(identifier);
            return Ok(data);
        }
        [HttpGet("gender/{identifier}")]
        public async Task<IActionResult> GetPokemonGender(string identifier)
        {
            var data = await unitOfWork.Pokemons.GetPokemonGender(identifier);
            return Ok(data);
        }

        [HttpGet("searchdetailed/{identifier}")]
        public async Task<IActionResult> SearchPokemonDetailed(string identifier)
        {
            var data = await unitOfWork.Pokemons.SearchPokemonDetailed(identifier);
            return Ok(data);
        }

        [HttpGet("moves_level/{identifier}")]
        public async Task<IActionResult> GetPokemonMovesByLevel(string identifier)
        {
            var data = await unitOfWork.Pokemons.GetPokemonMovesLevel(identifier);
            return Ok(data);
        }
        [HttpGet("move/{identifier}")]
        public async Task<IActionResult> GetPokemonByMove(string identifier)
        {
            var data = await unitOfWork.Pokemons.GetPokemonsByMove(identifier);
            return Ok(data);
        }
        [HttpGet("move/detail/{identifier}")]
        public async Task<IActionResult> GetMoveDetail(string identifier)
        {
            var data = await unitOfWork.Pokemons.GetMoveDetail(identifier);
            return Ok(data);
        }


        [HttpGet("moves")]
        public async Task<IActionResult> GetAllMoves()
        {
            var data = await unitOfWork.Moves.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("moves/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetMovesPaged(int pageNumber, int pageSize)
        {
            var data = await unitOfWork.Moves.GetAllPagedAsync(pageNumber, pageSize);
            return Ok(data);
        }
    }
}
