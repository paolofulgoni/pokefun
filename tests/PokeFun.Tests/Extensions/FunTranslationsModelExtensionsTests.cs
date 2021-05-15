using FluentAssertions;
using NUnit.Framework;
using PokeFun.FunTranslations.Model;

namespace PokeFun.Extensions.Tests
{
    public class FunTranslationsModelExtensionsTests
    {
        [Test]
        public void GetFixedTranslatedText_ReturnsSameTextAsInput_WhenNoDoubleSpacesNorMissingSpaceAfterFullStop()
        {
            // Arrange

            var content = new Contents { Translated = "First, second. Third." };

            // Act

            var translated = content.GetFixedTranslatedText();

            // Assert

            translated.Should().Be("First, second. Third.");
        }

        [Test]
        public void GetFixedTranslatedText_ReturnsTranslationWithoutDoubleSpaces_WhenTextContainsDoubleSpaces()
        {
            // Arrange

            var content = new Contents { Translated = "First,  second.  Third." };

            // Act

            var translated = content.GetFixedTranslatedText();

            // Assert

            translated.Should().Be("First, second. Third.");
        }

        [Test]
        public void GetFixedTranslatedText_ReturnsTranslationWithSpaceAfterFullStops_WhenSpaceAfterFullStopMissing()
        {
            // Arrange

            var content = new Contents { Translated = "First, second.Third... and fourth" };

            // Act

            var translated = content.GetFixedTranslatedText();

            // Assert

            translated.Should().Be("First, second. Third... and fourth");
        }
    }
}
