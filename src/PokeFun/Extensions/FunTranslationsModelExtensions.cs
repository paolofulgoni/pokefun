using System.Text.RegularExpressions;
using PokeFun.FunTranslations.Model;

namespace PokeFun.Extensions
{
    public static class FunTranslationsModelExtensions
    {
        /// <summary>
        /// Remove douple spaces and add missing spaces after  full stop (.) char
        /// </summary>
        /// <returns>The <see cref="Contents.Translated"/> text with corrections</returns>
        public static string GetFixedTranslatedText(this Contents contents)
        {
            var noDubleSpaces = contents.Translated.Replace("  ", " ");
            var fixSpaceAfterFullStop = Regex.Replace(noDubleSpaces, @"\.(\w)", ". $1");
            return fixSpaceAfterFullStop;
        }
    }
}
