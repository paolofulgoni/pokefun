using FluentAssertions;
using NUnit.Framework;
using PokeFun.PokeApi.Model;
using System;

namespace PokeFun.Extensions.Tests
{
    public class PokeApiModelExtensionsTests
    {
        [Test]
        public void GetFlavorTextByLanguageAndPreferredVersion_ReturnsExactMatch_WhenExactMatchExist()
        {
            // Arrange

            var flavorTextL1V1 = new FlavorText { Text = "t1", Language = new NamedAPIResource { Name = "l1" }, Version = new NamedAPIResource { Name = "v1" } };
            var flavorTextL1V2 = new FlavorText { Text = "t1", Language = new NamedAPIResource { Name = "l1" }, Version = new NamedAPIResource { Name = "v2" } };
            var flavorTextL1V3 = new FlavorText { Text = "t1", Language = new NamedAPIResource { Name = "l1" }, Version = new NamedAPIResource { Name = "v3" } };
            var flavorTextL2V2 = new FlavorText { Text = "t1", Language = new NamedAPIResource { Name = "l2" }, Version = new NamedAPIResource { Name = "v2" } };

            var pokemonSpecies = new PokemonSpecies
            {
                FlavorTextEntries = new[] { flavorTextL1V1, flavorTextL1V2, flavorTextL1V3, flavorTextL2V2 }
            };

            // Act

            var flavorText = pokemonSpecies.GetFlavorTextByLanguageAndPreferredVersion("l1", "v2");

            // Assert

            flavorText.Should().BeEquivalentTo(flavorTextL1V2);
        }

        [Test]
        public void GetFlavorTextByLanguageAndPreferredVersion_ReturnsRightLanguageButAnotherVersion_WhenExactMatchDoesNotExist()
        {
            // Arrange

            var flavorTextL1V1 = new FlavorText { Text = "t1", Language = new NamedAPIResource { Name = "l1" }, Version = new NamedAPIResource { Name = "v1" } };
            var flavorTextL1V2 = new FlavorText { Text = "t1", Language = new NamedAPIResource { Name = "l1" }, Version = new NamedAPIResource { Name = "v2" } };
            var flavorTextL1V3 = new FlavorText { Text = "t1", Language = new NamedAPIResource { Name = "l1" }, Version = new NamedAPIResource { Name = "v3" } };
            var flavorTextL2V2 = new FlavorText { Text = "t1", Language = new NamedAPIResource { Name = "l2" }, Version = new NamedAPIResource { Name = "v2" } };

            var pokemonSpecies = new PokemonSpecies
            {
                FlavorTextEntries = new[] { flavorTextL1V1, flavorTextL1V2, flavorTextL1V3, flavorTextL2V2 }
            };

            // Act

            var flavorText = pokemonSpecies.GetFlavorTextByLanguageAndPreferredVersion("l2", "v1");

            // Assert

            flavorText.Should().BeEquivalentTo(flavorTextL2V2);
        }

        [Test]
        public void GetFlavorTextByLanguageAndPreferredVersion_Throws_WhenNoMatchingLanguage()
        {
            // Arrange

            var flavorTextL1V1 = new FlavorText { Text = "t1", Language = new NamedAPIResource { Name = "l1" }, Version = new NamedAPIResource { Name = "v1" } };

            var pokemonSpecies = new PokemonSpecies
            {
                FlavorTextEntries = new[] { flavorTextL1V1 }
            };

            // Act

            Action action = () => pokemonSpecies.GetFlavorTextByLanguageAndPreferredVersion("l2", "v1");

            // Assert

            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetTextWithoutNewLineChars_ReturnsCleanedUpText_WhenTextContainsNewlineChars()
        {
            // Arrange

            var f = new FlavorText { Text = "This\nText\rContains \r\n NEWLINE CHARS"};

            // Act

            var cleanedText = f.GetTextWithoutNewLineChars();

            // Assert

            cleanedText.Should().Be("This Text Contains    NEWLINE CHARS");
        }
    }
}
