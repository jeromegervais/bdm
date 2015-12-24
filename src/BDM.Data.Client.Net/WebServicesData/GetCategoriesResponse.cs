using BDM.Common.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class GetCategoriesResponse : BaseResponse
    {
        [JsonProperty("data")]
        public GetCategoriesResponseData Data { get; set; }
    }

    internal class GetCategoriesResponseData
    {
        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }
    }
}
