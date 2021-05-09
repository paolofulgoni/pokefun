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
    public class PokemonControllerTest
    {
        [Test]
        public async Task Get_ReturnsPokemon_WhenPokemonNameIsValid()
        {
            // Arrange

            var controller = new PokemonController(NullLogger<PokemonController>.Instance);

            // Act

            var result = await controller.Get("mewtwo");

            // Assert

            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new Pokemon
            {
                Name = "mewtwo",
                Description = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.",
                Habitat = "rare",
                IsLegendary = true
            });
        }

        [Test]
        public async Task Get_ReturnsCode404NotFound_WhenPokemonNameIsUnknown()
        {
            // Arrange

            var controller = new PokemonController(NullLogger<PokemonController>.Instance);

            // Act

            var result = await controller.Get("doesnotexist");

            // Assert

            result.Result.Should().NotBeNull().And.BeAssignableTo<IStatusCodeActionResult>();
            ((IStatusCodeActionResult)result.Result).StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
