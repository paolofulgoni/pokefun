using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokeFun.FunTranslations.Model
{
    public class Success
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
