using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesMobile.Common.Models
{
    public class LanguageResponse
    {
        [JsonProperty("iso639_1")]
        public string Iso6391 { get; set; }

        [JsonProperty("iso639_2")]
        public string Iso6392 { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
    }
}
