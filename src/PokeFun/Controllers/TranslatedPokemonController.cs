using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeFun.Model;

namespace PokeFun.Controllers
{
    [ApiController]
    [Route("pokemon/translated")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class TranslatedPokemonController : ControllerBase
    {
        private readonly ILogger<TranslatedPokemonController> _logger;

        public TranslatedPokemonController(ILogger<TranslatedPokemonController> logger)
        {
            _logger = logger;
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
            // TODO
            throw new NotImplementedException();
        }
    }
}
