using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using PokeFun.Model;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokeFun.IntegrationTests
{
    [Category("Integration")]
    public class PokeFunIntegrationTests
    {
        [Test]
        public async Task GetPokemon_ReturnsPokemon_WhenPokemonNameIsValid()
        {
            // Arrange

            using var factory = new WebApplicationFactory<Startup>();
            using var client = factory.CreateClient();

            // Act

            var response = await client.GetAsync("/pokemon/mewtwo");

            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

            using var responseContent = await response.Content.ReadAsStreamAsync();
            var deserializedResponse = await JsonSerializer.DeserializeAsync<Pokemon>(responseContent);

            deserializedResponse.Should().BeEquivalentTo(new Pokemon
            {
                Name = "mewtwo",
                Description = "It was created by a scientist after years of horrific gene-splicing and DNA-engineering experiments.",
                Habitat = "rare",
                IsLegendary = true
            });
        }

        [Test]
        public async Task GetPokemon_ReturnsCode404NotFound_WhenPokemonNameIsUnknown()
        {
            // Arrange

            using var factory = new WebApplicationFactory<Startup>();
            using var client = factory.CreateClient();

            // Act

            var response = await client.GetAsync("/pokemon/doesnotexist");

            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
        }

        [Test]
        public async Task GetPokemonTranslated_ReturnsPokemonWithYodaTranslatedDescription_WhenPokemonNameIsLegendary()
        {
            // Arrange

            using var factory = new WebApplicationFactory<Startup>();
            using var client = factory.CreateClient();

            // Act

            var response = await client.GetAsync("/pokemon/translated/mewtwo");

            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

            using var responseContent = await response.Content.ReadAsStreamAsync();
            var deserializedResponse = await JsonSerializer.DeserializeAsync<Pokemon>(responseContent);

            deserializedResponse.Should().BeEquivalentTo(new Pokemon
            {
                Name = "mewtwo",
                Description = "Created by a scientist after years of horrific gene-splicing and dna-engineering experiments, it was.",
                Habitat = "rare",
                IsLegendary = true
            });
        }

        [Test]
        public async Task GetPokemonTranslated_ReturnsPokemonWithYodaTranslatedDescription_WhenPokemonNameIsCave()
        {
            // Arrange

            using var factory = new WebApplicationFactory<Startup>();
            using var client = factory.CreateClient();

            // Act

            var response = await client.GetAsync("/pokemon/translated/diglett");

            // Assert


            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

            using var responseContent = await response.Content.ReadAsStreamAsync();
            var deserializedResponse = await JsonSerializer.DeserializeAsync<Pokemon>(responseContent);

            deserializedResponse.Should().BeEquivalentTo(new Pokemon
            {
                Name = "diglett",
                Description = "On plant roots, lives about one yard underground where it feeds. Aboveground, it sometimes appears.",
                Habitat = "cave",
                IsLegendary = false
            });
        }

        [Test]
        public async Task GetPokemonTranslated_ReturnsPokemonWithShakespeareTranslatedDescription_WhenPokemonNameIsNotLegendaryNorCave()
        {
            // Arrange

            using var factory = new WebApplicationFactory<Startup>();
            using var client = factory.CreateClient();

            // Act

            var response = await client.GetAsync("/pokemon/translated/ditto");

            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

            using var responseContent = await response.Content.ReadAsStreamAsync();
            var deserializedResponse = await JsonSerializer.DeserializeAsync<Pokemon>(responseContent);

            deserializedResponse.Should().BeEquivalentTo(new Pokemon
            {
                Name = "ditto",
                Description = "'t hath the ability to reconstitute its entire cellular structure to transform into thither's few or none will entertain it 't sees.",
                Habitat = "urban",
                IsLegendary = false
            });
        }

        [Test]
        public async Task GetPokemonTranslated_ReturnsCode404NotFound_WhenPokemonNameIsUnknown()
        {
            // Arrange

            using var factory = new WebApplicationFactory<Startup>();
            using var client = factory.CreateClient();

            // Act

            var response = await client.GetAsync("/pokemon/translated/doesnotexist");

            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
        }
    }
}
