using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeFun.Model;
using PokeFun.PokeApi;

namespace PokeFun.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokeApiService _pokeApiService;

        public PokemonController(ILogger<PokemonController> logger, IPokeApiService pokeApiService)
        {
            _logger = logger;
            _pokeApiService = pokeApiService;
        }

        /// <summary>
        /// Get some basic information about a Pokemon, given it's name.
        /// </summary>
        /// <param name="pokemonName">Name of the pokemon</param>
        /// <returns>Basic Pokemon information</returns>
        [HttpGet("{pokemonName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pokemon>> Get(string pokemonName)
        {
            try
            {
                var pokemon = await _pokeApiService.GetPokemon(pokemonName);
                var species = await _pokeApiService.GetPokemonSpecies(pokemon.Species.Name);

                // suppose there is always at least one English flavor text, prefer the "x" version
                var englishFlavorText = species.FlavorTextEntries.Where(t => t.Language.Name.Equals("en", StringComparison.OrdinalIgnoreCase));
                var bestEnglishFlavorText = englishFlavorText.FirstOrDefault(t => t.Version.Name == "x") ?? englishFlavorText.First();

                var cleanedFlavorText = Regex.Replace(bestEnglishFlavorText.Text, @"\\[rn]", " ");

                return new Pokemon
                {
                    Name = pokemon.Name,
                    Description = cleanedFlavorText,
                    Habitat = species.Habitat?.Name,
                    IsLegendary = species.IsLegendary
                };
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogInformation(ex, "{PokemonName} not found", pokemonName);

                return NotFound();
            }
        }
    }
}
