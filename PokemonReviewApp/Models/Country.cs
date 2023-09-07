using System.Text.Json;

using System.Text.Json.Serialization;

namespace PokemonReviewApp.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Owner>? Owners { get; set;}
    }
}
