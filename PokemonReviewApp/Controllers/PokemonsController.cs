using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;
using System.Diagnostics.CodeAnalysis;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        public PokemonsController(IPokemonRepository pokemonRepository)
        {
              _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _pokemonRepository.GetPokemons();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        public IActionResult GetPokemon(int id)
        {
            if (!_pokemonRepository.PokemonExists(id)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var pokemon = _pokemonRepository.GetPokemon(id);
            return Ok(pokemon);
        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))] 
        public IActionResult GetPokemonRating(int id) { 
            if(!_pokemonRepository.PokemonExists(id)) return NotFound();
            var rating = _pokemonRepository.GetPokemonRating(id);
            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] Pokemon pokemon)
        {
            if (pokemon == null) return BadRequest();
            var existingPokemon = _pokemonRepository.GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemon.Name.Trim().ToUpper()).FirstOrDefault();
            if (existingPokemon != null)
            {
                ModelState.AddModelError("", "Create Already Exists");
                return StatusCode(422, ModelState);
            };

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _pokemonRepository.CreatePokemon(ownerId, categoryId, pokemon);

            return Ok("Successfully Created");
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePokemon(int id, Pokemon pokemon)
        {
            if (id != pokemon.Id) return BadRequest();
            if (pokemon is null) return BadRequest();
            if (!_pokemonRepository.PokemonExists(id)) return NotFound();

            _pokemonRepository.UpdatePokemon(id, pokemon);
            return Ok("Successfully updated");
        }
    }
}
