using BDM.Common.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class SearchResponse : BaseResponse
    {
        [JsonProperty("blagues")]
        public List<Blague> Blagues { get; set; }
    }
}
