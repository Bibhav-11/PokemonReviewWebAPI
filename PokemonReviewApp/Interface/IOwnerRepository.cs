using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interface
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        bool OwnerExists(int ownerId);

        ICollection<Pokemon> GetPokemonsByOwner(int ownerId);
    }
}
