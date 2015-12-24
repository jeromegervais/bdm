using Newtonsoft.Json;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class GetBlaguesResponse : BaseResponse
    {
        [JsonProperty("data")]
        public GetBlaguesResponseData Data { get; set; }
    }
}
