using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        bool CategoriesExists(int id);

        bool AddCategory(Category category);

        bool UpdateCategory(int id, Category category);

        bool DeleteCategory(Category category);
        bool Save();
    }
}
