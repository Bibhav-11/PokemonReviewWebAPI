using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interface
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        ICollection<Owner> GetOwnersByCountry(int countryId);
        bool CountryExists(int countryId);

        bool CreateCountry(Country country);
        bool Save();
    }
}
