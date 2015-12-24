using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class GetBlaguesForCategoryResponse : BaseResponse
    {
        [JsonProperty("blagues")]
        public GetBlaguesResponseData Data { get; set; }
    }
}
