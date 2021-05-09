using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using PokeFun.Controllers;
using PokeFun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeFun.Tests.Controllers
{
    public class TranslatedPokemonControllerTest
    {
        [Test]
        public async Task Get_ReturnsPokemonWithYodaTranslatedDescription_WhenPokemonNameIsLegendary()
        {
            // Arrange

            var controller = new TranslatedPokemonController(NullLogger<TranslatedPokemonController>.Instance);

            // Act

            var result = await controller.Get("mewtwo");

            // Assert

            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new Pokemon
            {
                Name = "mewtwo",
                Description = "Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.",
                Habitat = "rare",
                IsLegendary = true
            });
        }

        [Test]
        public async Task Get_ReturnsPokemonWithYodaTranslatedDescription_WhenPokemonNameIsCave()
        {
            // Arrange

            var controller = new TranslatedPokemonController(NullLogger<TranslatedPokemonController>.Instance);

            // Act

            var result = await controller.Get("diglett");

            // Assert

            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new Pokemon
            {
                Name = "diglett",
                Description = "On plant roots,  lives about one yard underground where it feeds.Above ground,  it sometimes appears.",
                Habitat = "cave",
                IsLegendary = false
            });
        }

        [Test]
        public async Task Get_ReturnsPokemonWithShakespeareTranslatedDescription_WhenPokemonNameIsNotLegendaryNorCave()
        {
            // Arrange

            var controller = new TranslatedPokemonController(NullLogger<TranslatedPokemonController>.Instance);

            // Act

            var result = await controller.Get("ditto");

            // Assert

            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new Pokemon
            {
                Name = "ditto",
                Description = "Capable of copying an foe's genetic code to instantly transform itself into a duplicate of the foe.",
                Habitat = "urban",
                IsLegendary = false
            });
        }

        [Test]
        public async Task Get_ReturnsCode404NotFound_WhenPokemonNameIsUnknown()
        {
            // Arrange

            var controller = new TranslatedPokemonController(NullLogger<TranslatedPokemonController>.Instance);

            // Act

            var result = await controller.Get("doesnotexist");

            // Assert

            result.Result.Should().NotBeNull().And.BeAssignableTo<IStatusCodeActionResult>();
            ((IStatusCodeActionResult)result.Result).StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
