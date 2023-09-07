using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountries();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountry(int id)
        {
            if (!_countryRepository.CountryExists(id)) return NotFound();
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var country = _countryRepository.GetCountry(id);
            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] Country country)
        {
            if (country == null) return BadRequest();
            var existingCategory = _countryRepository.GetCountries().Where(c => c.Name.Trim().ToUpper() == country.Name.Trim().ToUpper()).FirstOrDefault();
            if (existingCategory != null)
            {
                ModelState.AddModelError("", "Create Already Exists");
                return StatusCode(422, ModelState);
            };

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _countryRepository.CreateCountry(country);

            return Ok("Successfully Created");
        }

    }
}
