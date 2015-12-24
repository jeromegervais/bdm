using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Common.Model
{
    public class Blague : IdObject
    {
        [JsonProperty("blague")]
        public string Text { get; set; }

        [JsonProperty("pseudo")]
        public string Author { get; set; }

        [JsonProperty("good")]
        public int NbGoods { get; set; }

        [JsonProperty("bad")]
        public int NbBads { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        [JsonProperty("date_published")]
        public DateTime PublicationDate { get; set; }
    }
}
