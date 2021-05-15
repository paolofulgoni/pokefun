using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PokeFun.FunTranslations.Tests
{
    public class FunTranslationsServiceTest
    {
        [Test]
        public async Task TranslateEnglishToShakespeare_ReturnsValidTranslation_WhenThirdPartyServiceResponseIsOk()
        {
            // Arrange

            var content = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "shakespeare.json"));

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

            var optionsMock = new Mock<IOptions<FunTranslationsServiceOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new FunTranslationsServiceOptions());

            var service = new FunTranslationsService(clientMock, optionsMock.Object);

            // Act

            var result = await service.TranslateEnglishToShakespeare("I think Pokemons fight for fun");

            // Assert

            result.Should().NotBeNull();
            result.Success.Total.Should().Be(1);
            result.Contents.Text.Should().Be("I think Pokemons fight for fun");
            result.Contents.Translated.Should().Be("I bethink pokemons square for excit'ment");
            result.Contents.Translation.Should().Be("shakespeare");
        }

        [Test]
        public async Task TranslateEnglishToYoda_ReturnsValidTranslation_WhenThirdPartyServiceResponseIsOk()
        {
            // Arrange

            var content = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "yoda.json"));

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

            var optionsMock = new Mock<IOptions<FunTranslationsServiceOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new FunTranslationsServiceOptions());

            var service = new FunTranslationsService(clientMock, optionsMock.Object);

            // Act

            var result = await service.TranslateEnglishToShakespeare("I think Pokemons fight for fun");

            // Assert

            result.Should().NotBeNull();
            result.Success.Total.Should().Be(1);
            result.Contents.Text.Should().Be("I think Pokemons fight for fun");
            result.Contents.Translated.Should().Be("Pokemons fight for fun,  I think");
            result.Contents.Translation.Should().Be("yoda");
        }

        [Test]
        public void TranslateEnglishToShakespeare_ThrowsHttpRequestException_WhenThirdPartyServiceResponseIsTooManyRequests()
        {
            // Arrange

            var content = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "too-many-requests.json"));

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.TooManyRequests,
                    Content = new StringContent(content),
                })
               .Verifiable();

            var clientMock = new HttpClient(handlerMock.Object);

            var optionsMock = new Mock<IOptions<FunTranslationsServiceOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new FunTranslationsServiceOptions());

            var service = new FunTranslationsService(clientMock, optionsMock.Object);

            // Act

            Func<Task> act = async () => { await service.TranslateEnglishToShakespeare("I think Pokemons fight for fun"); };

            // Assert

            act.Should().Throw<HttpRequestException>().Where(e => e.StatusCode == HttpStatusCode.TooManyRequests);
        }

        [Test]
        public void TranslateEnglishToYoda_ThrowsHttpRequestException_WhenThirdPartyServiceResponseIsTooManyRequests()
        {
            // Arrange

            var content = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "too-many-requests.json"));

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.TooManyRequests,
                    Content = new StringContent(content),
                })
               .Verifiable();

            var clientMock = new HttpClient(handlerMock.Object);

            var optionsMock = new Mock<IOptions<FunTranslationsServiceOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new FunTranslationsServiceOptions());

            var service = new FunTranslationsService(clientMock, optionsMock.Object);

            // Act

            Func<Task> act = async () => { await service.TranslateEnglishToYoda("I think Pokemons fight for fun"); };

            // Assert

            act.Should().Throw<HttpRequestException>().Where(e => e.StatusCode == HttpStatusCode.TooManyRequests);
        }
    }
}
