using BDM.Common.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class GetBlaguesResponseData
    {
        [JsonProperty("last")]
        public List<Blague> Last { get; set; }

        [JsonProperty("top_week")]
        public List<Blague> TopWeek { get; set; }

        [JsonProperty("top_month")]
        public List<Blague> TopMonth { get; set; }

        [JsonProperty("top_year")]
        public List<Blague> TopYear { get; set; }

        [JsonProperty("random")]
        public List<Blague> Random { get; set; }

    }
}
