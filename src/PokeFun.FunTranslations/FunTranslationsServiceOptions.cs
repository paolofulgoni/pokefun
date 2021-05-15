using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeFun.FunTranslations
{
    public class FunTranslationsServiceOptions
    {
        public const string OptionsName = "FunTranslationsService";

        public string BaseAddress { get; set; } = "https://api.funtranslations.com";
        public string ShakespeareEndpoint { get; set; } = "translate/shakespeare.json";
        public string YodaEndpoint { get; set; } = "translate/yoda.json";
    }
}
