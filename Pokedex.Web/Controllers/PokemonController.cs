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
    }
}
