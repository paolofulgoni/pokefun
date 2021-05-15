using Microsoft.Extensions.Options;
using PokeFun.FunTranslations.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokeFun.FunTranslations
{
    /// <summary>
    /// Provides fun translations via the <see href="https://funtranslations.com/">Fun Translations</see> API 
    /// </summary>
    public interface IFunTranslationsService
    {
        /// <summary>
        /// Translate a text from English to Shakespeare
        /// </summary>
        /// <param name="text">Text to translate</param>
        /// <returns>The translation response</returns>
        /// <exception cref="HttpRequestException">Thrown when the third-party service doesn't return an Success status code</exception>
        Task<Translation> TranslateEnglishToShakespeare(string text);

        /// <summary>
        /// Translate a text from English to Yoda
        /// </summary>
        /// <param name="text">Text to translate</param>
        /// <returns>The translation response</returns>
        /// <exception cref="HttpRequestException">Thrown when the third-party service doesn't return an Success status code</exception>
        Task<Translation> TranslateEnglishToYoda(string text);
    }

    public class FunTranslationsService : IFunTranslationsService
    {
        private readonly FunTranslationsServiceOptions _options;

        public HttpClient Client { get; }

        public FunTranslationsService(HttpClient client, IOptions<FunTranslationsServiceOptions> options)
        {
            _options = options.Value;

            client.BaseAddress = new Uri(options.Value.BaseAddress);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;
        }

        public async Task<Translation> TranslateEnglishToShakespeare(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));

            return await Translate(_options.ShakespeareEndpoint, text);
        }

        public async Task<Translation> TranslateEnglishToYoda(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));

            return await Translate(_options.YodaEndpoint, text);
        }

        private async Task<Translation> Translate(string requestUri, string text)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("text", text)
            });

            var response = await Client.PostAsync(requestUri, content);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Translation>(responseStream);
        }
    }
}
