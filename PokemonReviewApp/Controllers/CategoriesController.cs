using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            if(!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoriesExists(id)) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
            var category = _categoryRepository.GetCategory(id);
            return Ok(category);
        }

        [HttpGet("{categoryId}/pokemons")]
        [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
        public IActionResult GetPokemonsByCategory(int categoryId)
        {
            if(!_categoryRepository.CategoriesExists(categoryId)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var pokemons = _categoryRepository.GetPokemonByCategory(categoryId);
            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            if(category == null) return BadRequest();
            var existingCategory = _categoryRepository.GetCategories().Where(c => c.Name.Trim().ToUpper() == category.Name.Trim().ToUpper()).FirstOrDefault(); 
            if (existingCategory != null)
            {
                ModelState.AddModelError("", "Create Already Exists");
                return StatusCode(422, ModelState);
            };

            if(!ModelState.IsValid) return BadRequest(ModelState);

            _categoryRepository.AddCategory(category);

            return Ok("Successfully Created");
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {
            if(id != category.Id) return BadRequest();
            if(category is null) return BadRequest();
            if (!_categoryRepository.CategoriesExists(id)) return NotFound();

            _categoryRepository.UpdateCategory(id, category);
            return Ok("Successfully updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if(!_categoryRepository.CategoriesExists(id)) return BadRequest();
            var category = _categoryRepository.GetCategory(id);
            if (category == null) return BadRequest();

            _categoryRepository.DeleteCategory(category);
            return Ok("Successfully Deleted");
            
        }

    }
}
