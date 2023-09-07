﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace PokemonReviewApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<PokemonCategory>? PokemonCategories { get; set; }
    }
}
