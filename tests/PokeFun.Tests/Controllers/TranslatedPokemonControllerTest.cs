using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using PokeFun.Controllers;
using PokeFun.FunTranslations;
using PokeFun.FunTranslations.Model;
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
    public class TranslatedPokemonControllerTest
    {
        [Test]
        public async Task Get_ReturnsPokemonWithYodaTranslatedDescription_WhenPokemonNameIsLegendary()
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

            var funTranslationsMock = new Mock<IFunTranslationsService>();
            funTranslationsMock
                .Setup(m => m.TranslateEnglishToYoda("desc en x").Result)
                .Returns(new Translation
                {
                    Contents = new Contents
                    {
                        Text = "desc en x",
                        Translated = "desc yoda x",
                        Translation = "yoda"
                    },
                    Success = new Success { Total = 1 }
                });

            var controller = new TranslatedPokemonController(NullLogger<TranslatedPokemonController>.Instance, pokeApiMock.Object, funTranslationsMock.Object);

            // Act

            var result = await controller.Get("poke");

            // Assert

            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new Pokemon
            {
                Name = "poke",
                Description = "desc yoda x",
                Habitat = "habitat",
                IsLegendary = true
            });
        }

        [Test]
        public async Task Get_ReturnsPokemonWithYodaTranslatedDescription_WhenPokemonNameIsCave()
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
                    Habitat = new PokeApi.Model.NamedAPIResource { Name = "cave" },
                    IsLegendary = false
                });

            var funTranslationsMock = new Mock<IFunTranslationsService>();
            funTranslationsMock
                .Setup(m => m.TranslateEnglishToYoda("desc en x").Result)
                .Returns(new Translation
                {
                    Contents = new Contents
                    {
                        Text = "desc en x",
                        Translated = "desc yoda x",
                        Translation = "yoda"
                    },
                    Success = new Success { Total = 1 }
                });

            var controller = new TranslatedPokemonController(NullLogger<TranslatedPokemonController>.Instance, pokeApiMock.Object, funTranslationsMock.Object);

            // Act

            var result = await controller.Get("poke");

            // Assert

            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new Pokemon
            {
                Name = "poke",
                Description = "desc yoda x",
                Habitat = "cave",
                IsLegendary = false
            });
        }

        [Test]
        public async Task Get_ReturnsPokemonWithShakespeareTranslatedDescription_WhenPokemonNameIsNotLegendaryNorCave()
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
                    IsLegendary = false
                });

            var funTranslationsMock = new Mock<IFunTranslationsService>();
            funTranslationsMock
                .Setup(m => m.TranslateEnglishToShakespeare("desc en x").Result)
                .Returns(new Translation
                {
                    Contents = new Contents
                    {
                        Text = "desc en x",
                        Translated = "desc shakespeare x",
                        Translation = "shakespeare"
                    },
                    Success = new Success { Total = 1 }
                });

            var controller = new TranslatedPokemonController(NullLogger<TranslatedPokemonController>.Instance, pokeApiMock.Object, funTranslationsMock.Object);

            // Act

            var result = await controller.Get("poke");

            // Assert

            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new Pokemon
            {
                Name = "poke",
                Description = "desc shakespeare x",
                Habitat = "habitat",
                IsLegendary = false
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

            var funTranslationsMock = new Mock<IFunTranslationsService>();

            var controller = new TranslatedPokemonController(NullLogger<TranslatedPokemonController>.Instance, pokeApiMock.Object, funTranslationsMock.Object);

            // Act

            var result = await controller.Get("doesnotexist");

            // Assert

            result.Result.Should().NotBeNull().And.BeAssignableTo<IStatusCodeActionResult>();
            ((IStatusCodeActionResult)result.Result).StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
