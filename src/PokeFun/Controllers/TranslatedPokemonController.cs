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
using PokeFun.Extensions;
using PokeFun.FunTranslations;
using PokeFun.Model;
using PokeFun.PokeApi;

namespace PokeFun.Controllers
{
    [ApiController]
    [Route("pokemon/translated")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class TranslatedPokemonController : ControllerBase
    {
        private readonly ILogger<TranslatedPokemonController> _logger;
        private readonly IPokeApiService _pokeApiService;
        private readonly IFunTranslationsService _funTranslationsService;

        public TranslatedPokemonController(ILogger<TranslatedPokemonController> logger, IPokeApiService pokeApiService, IFunTranslationsService funTranslationsService)
        {
            _logger = logger;
            _pokeApiService = pokeApiService;
            _funTranslationsService = funTranslationsService;
        }

        /// <summary>
        /// Get some basic information about a Pokemon, given it's name, with fun translation of its description.
        /// </summary>
        /// <param name="pokemonName">Name of the pokemon</param>
        /// <returns>Basic Pokemon information</returns>
        [HttpGet("{pokemonName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pokemon>> Get(string pokemonName)
        {
            Pokemon response;

            try
            {
                var pokemon = await _pokeApiService.GetPokemon(pokemonName);
                var species = await _pokeApiService.GetPokemonSpecies(pokemon.Species.Name);

                // suppose there is always at least one English flavor text
                var flavorText = species.GetFlavorTextByLanguageAndPreferredVersion("en", "x");
                var cleanedFlavorText = flavorText.GetTextWithoutNewLineChars();

                response = new Pokemon
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

            try
            {
                if (response.Habitat == "cave" || response.IsLegendary)
                {
                    var translation = await _funTranslationsService.TranslateEnglishToYoda(response.Description);
                    response = response with { Description = translation.Contents.Translated };
                }
                else
                {
                    var translation = await _funTranslationsService.TranslateEnglishToShakespeare(response.Description);
                    response = response with { Description = translation.Contents.Translated };
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to translate {PokemonName}'s description, using the standard description", pokemonName);
            }

            return response;
        }
    }
}
