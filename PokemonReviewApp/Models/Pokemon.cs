using System.Text.Json.Serialization;

namespace PokemonReviewApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        [JsonIgnore]
        public ICollection<Review>? Reviews { get; set; }

        [JsonIgnore]
        public ICollection<PokemonCategory>? PokemonCategories { get; set; }
        [JsonIgnore]
        public ICollection<PokemonOwner>? PokemonOwners { get; set; }

    }
}
