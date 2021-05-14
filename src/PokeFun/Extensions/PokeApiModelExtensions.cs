using PokeFun.PokeApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokeFun.Extensions
{
    public static class PokeApiModelExtensions
    {
        public static FlavorText GetFlavorTextByLanguageAndPreferredVersion(this PokemonSpecies species, string language, string version)
        {
            var flavorsFilteredByLanguage = species.FlavorTextEntries.Where(t => t.Language.Name.Equals(language, StringComparison.OrdinalIgnoreCase));
            return flavorsFilteredByLanguage.FirstOrDefault(t => t.Version.Name == version) ?? flavorsFilteredByLanguage.First();
        }

        public static string GetTextWithoutNewLineChars(this FlavorText flavorText)
        {
            return Regex.Replace(flavorText.Text, @"[\r\n]", " ");
        }
    }
}
