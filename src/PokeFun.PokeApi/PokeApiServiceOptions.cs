namespace PokeFun.PokeApi
{
    public class PokeApiServiceOptions
    {
        public const string OptionsName = "PokeApiService";

        public string BaseAddress { get; set; } = "https://pokeapi.co";
        public string PokemonEndpoint { get; set; } = "api/v2/pokemon";
        public string PokemonSpeciesEndpoint { get; set; } = "api/v2/pokemon-species";
    }
}
