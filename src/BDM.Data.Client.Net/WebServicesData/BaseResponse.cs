using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Data.Client.Net.WebServicesData
{
    /// <summary>
    /// Classe de base pour toutes les reponses recues en appelant les WebSerices
    /// </summary>
	internal class BaseResponse
	{
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
	}

    internal class Meta
    {
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
