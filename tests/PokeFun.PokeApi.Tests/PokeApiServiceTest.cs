using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using PokeFun.PokeApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PokeFun.PokeApi.Tests
{
    public class PokeApiServiceTest
    {
        [Test]
        public async Task GetPokemon_ReturnsValidData_WhenThirdPartyServiceResponseIsOk()
        {
            // Arrange

            // NOTE: the json file contains only some relevant parts of the real third-party API response
            var content = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "wormadam-plant-pokemon.json"));

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content),
                })
               .Verifiable();

            var clientMock = new HttpClient(handlerMock.Object);

            var optionsMock = new Mock<IOptions<PokeApiServiceOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new PokeApiServiceOptions());

            var service = new PokeApiService(clientMock, optionsMock.Object);

            // Act

            var result = await service.GetPokemon("wormadam-plant");

            // Assert

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new Pokemon
            {
                Id = 413,
                Name = "wormadam-plant",
                Species = new NamedAPIResource
                {
                    Name = "wormadam"
                }
            });
        }
        [Test]
        public async Task GetPokemonSpecies_ReturnsValidData_WhenThirdPartyServiceResponseIsOk()
        {
            // Arrange

            // NOTE: the json file contains only some relevant parts of the real third-party API response
            var content = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "ditto-species.json"));

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content),
                })
               .Verifiable();

            var clientMock = new HttpClient(handlerMock.Object);

            var optionsMock = new Mock<IOptions<PokeApiServiceOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new PokeApiServiceOptions());

            var service = new PokeApiService(clientMock, optionsMock.Object);

            // Act

            var result = await service.GetPokemonSpecies("ditto");

            // Assert

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new PokemonSpecies
            {
                Id = 132,
                Name = "ditto",
                FlavorTextEntries = new List<FlavorText>
                {
                    new FlavorText
                    {
                        Text = "It can freely recombine its own cellular structure to\ntransform into other life-forms.",
                        Language = new NamedAPIResource
                        {
                            Name = "en"
                        },
                        Version = new NamedAPIResource
                        {
                            Name = "y"
                        }
                    }
                },
                Habitat = new NamedAPIResource
                {
                    Name = "urban"
                },
                IsLegendary = false,
            });
        }

        [Test]
        public void GetPokemon_ThrowsHttpRequestException_WhenThirdPartyServiceResponseIsNotFound()
        {
            // Arrange

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Not Found"),
                })
               .Verifiable();

            var clientMock = new HttpClient(handlerMock.Object);

            var optionsMock = new Mock<IOptions<PokeApiServiceOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new PokeApiServiceOptions());

            var service = new PokeApiService(clientMock, optionsMock.Object);

            // Act

            Func<Task> act = async () => { await service.GetPokemon("doesnotexist"); };

            // Assert

            act.Should().Throw<HttpRequestException>().Where(e => e.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public void GetPokemonSpecies_ThrowsHttpRequestException_WhenThirdPartyServiceResponseIsNotFound()
        {
            // Arrange

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Not Found"),
                })
               .Verifiable();

            var clientMock = new HttpClient(handlerMock.Object);

            var optionsMock = new Mock<IOptions<PokeApiServiceOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new PokeApiServiceOptions());

            var service = new PokeApiService(clientMock, optionsMock.Object);

            // Act

            Func<Task> act = async () => { await service.GetPokemonSpecies("doesnotexist"); };

            // Assert

            act.Should().Throw<HttpRequestException>().Where(e => e.StatusCode == HttpStatusCode.NotFound);
        }
    }
}
