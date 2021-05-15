using Microsoft.Extensions.Options;
using PokeFun.PokeApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokeFun.PokeApi
{
    /// <summary>
    /// Provides Pokemon information via <see href="https://pokeapi.co/">PokeApi</see>
    /// </summary>
    public interface IPokeApiService
    {
        /// <summary>
        /// Get Pokemon's information
        /// </summary>
        /// <param name="pokemonName">Name of the Pokemon</param>
        /// <exception cref="HttpRequestException">Thrown when the third-party service doesn't return an Success status code</exception>
        Task<Pokemon> GetPokemon(string pokemonName);

        /// <summary>
        /// Get Pokemon species' information
        /// </summary>
        /// <param name="pokemonSpeciesName">Name of the Pokemon species</param>
        /// <exception cref="HttpRequestException">Thrown when the third-party service doesn't return an Success status code</exception>
        Task<PokemonSpecies> GetPokemonSpecies(string pokemonSpeciesName);
    }

    public class PokeApiService : IPokeApiService
    {
        private readonly PokeApiServiceOptions _options;

        public HttpClient Client { get; }

        public PokeApiService(HttpClient client, IOptions<PokeApiServiceOptions> options)
        {
            _options = options.Value;

            client.BaseAddress = new Uri(options.Value.BaseAddress);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;
        }

        public async Task<Pokemon> GetPokemon(string pokemonName)
        {
            if (pokemonName == null) throw new ArgumentNullException(nameof(pokemonName));

            return await GetAndDeserialize<Pokemon>($"{_options.PokemonEndpoint}/{pokemonName}");
        }

        public async Task<PokemonSpecies> GetPokemonSpecies(string pokemonSpeciesName)
        {
            if (pokemonSpeciesName == null) throw new ArgumentNullException(nameof(pokemonSpeciesName));

            return await GetAndDeserialize<PokemonSpecies>($"{_options.PokemonSpeciesEndpoint}/{pokemonSpeciesName}");
        }

        private async Task<T> GetAndDeserialize<T>(string requestUri)
        {
            var response = await Client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(responseStream);
        }
    }
}
