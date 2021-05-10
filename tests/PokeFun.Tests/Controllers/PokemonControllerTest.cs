using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using PokeFun.Controllers;
using PokeFun.Model;
using PokeFun.PokeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            var pokeApiMock = new Mock<IPokeApiService>();
            pokeApiMock
                .Setup(m => m.GetPokemon("poke").Result)
                .Returns(new PokeApi.Model.Pokemon
                {
                    Id = 413,
                    Name = "poke",
                    Species = new PokeApi.Model.NamedAPIResource { Name = "poke-species" }
                });
            pokeApiMock
                .Setup(m => m.GetPokemonSpecies("poke-species").Result)
                .Returns(new PokeApi.Model.PokemonSpecies
                {
                    Id = 413,
                    Name = "poke-species",
                    FlavorTextEntries = new List<PokeApi.Model.FlavorText>
                    {
                        new PokeApi.Model.FlavorText
                        {
                            Text = "desc en y",
                            Language = new PokeApi.Model.NamedAPIResource { Name = "en" },
                            Version = new PokeApi.Model.NamedAPIResource { Name = "y" },
                        },
                        new PokeApi.Model.FlavorText
                        {
                            Text = "desc en x",
                            Language = new PokeApi.Model.NamedAPIResource { Name = "en" },
                            Version = new PokeApi.Model.NamedAPIResource { Name = "x" },
                        },
                        new PokeApi.Model.FlavorText
                        {
                            Text = "desc it x",
                            Language = new PokeApi.Model.NamedAPIResource { Name = "it" },
                            Version = new PokeApi.Model.NamedAPIResource { Name = "x" },
                        },
                    },
                    Habitat = new PokeApi.Model.NamedAPIResource { Name = "habitat" },
                    IsLegendary = true
                });

            var controller = new PokemonController(NullLogger<PokemonController>.Instance, pokeApiMock.Object);

            // Act

            var result = await controller.Get("poke");

            // Assert

            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new Pokemon
            {
                Name = "poke",
                Description = "desc en x",
                Habitat = "habitat",
                IsLegendary = true
            });
        }

        [Test]
        public async Task Get_ReturnsCode404NotFound_WhenPokemonNameIsUnknown()
        {
            // Arrange

            var pokeApiMock = new Mock<IPokeApiService>();
            pokeApiMock
                .Setup(m => m.GetPokemon(It.IsAny<string>()).Result)
                .Throws(new HttpRequestException(null, null, HttpStatusCode.NotFound));
            pokeApiMock
                .Setup(m => m.GetPokemonSpecies(It.IsAny<string>()).Result)
                .Throws(new HttpRequestException(null, null, HttpStatusCode.NotFound));

            var controller = new PokemonController(NullLogger<PokemonController>.Instance, pokeApiMock.Object);

            // Act

            var result = await controller.Get("doesnotexist");

            // Assert

            result.Result.Should().NotBeNull().And.BeAssignableTo<IStatusCodeActionResult>();
            ((IStatusCodeActionResult)result.Result).StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
