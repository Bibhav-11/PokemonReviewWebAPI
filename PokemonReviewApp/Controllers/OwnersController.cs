using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        public OwnersController(IOwnerRepository repository)
        {
            _ownerRepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Owner>))]
        public IActionResult GetOwners()
        {
            var owners = _ownerRepository.GetOwners();
            if (!ModelState.IsValid) return BadRequest();
            return Ok(owners);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        public IActionResult GetOwner(int id) {
            if (!_ownerRepository.OwnerExists(id)) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
            return Ok(_ownerRepository.GetOwner(id));
        }

        [HttpGet("{id}/pokemons")]
        [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
        public IActionResult GetPokemons(int id)
        {
            if (!_ownerRepository.OwnerExists(id)) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
            return Ok(_ownerRepository.GetPokemonsByOwner(id));
        }
    }
}
