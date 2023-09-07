﻿using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interface
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();

        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);

        decimal GetPokemonRating(int pokeId);

        bool PokemonExists(int id);

        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);

        bool UpdatePokemon(int id,  Pokemon pokemon);
        bool Save();
    }
}