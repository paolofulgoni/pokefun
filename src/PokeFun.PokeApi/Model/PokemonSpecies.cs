using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokeFun.PokeApi.Model
{
    public record PokemonSpecies
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("flavor_text_entries")]
        public IEnumerable<FlavorText> FlavorTextEntries { get; init; }

        [JsonPropertyName("habitat")]
        public NamedAPIResource Habitat { get; init; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; init; }

        public virtual bool Equals(PokemonSpecies other)
        {
            return other is not null
                && EqualityComparer<int>.Default.Equals(Id, other.Id)
                && EqualityComparer<string>.Default.Equals(Name, other.Name)
                && FlavorTextEntries.SequenceEqual(other.FlavorTextEntries)
                && EqualityComparer<NamedAPIResource>.Default.Equals(Habitat, other.Habitat)
                && EqualityComparer<bool>.Default.Equals(IsLegendary, other.IsLegendary);
        }

        public override int GetHashCode()
        {
            HashCode hashcode = new();
            hashcode.Add(Id);
            hashcode.Add(Name);
            foreach (var item in FlavorTextEntries)
            {
                hashcode.Add(item);
            }
            hashcode.Add(Habitat);
            hashcode.Add(IsLegendary);

            return hashcode.ToHashCode();
        }
    }
}
