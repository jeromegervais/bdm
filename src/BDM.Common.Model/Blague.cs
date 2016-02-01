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
        public string Text { get { return TextNew ?? TextOld; } }

        public string Author { get { return AuthorNew ?? AuthorOld; } }

        public int NbGoods { get { return NbGoodsNew + NbGoodsOld; } }

        public int NbBads { get { return NbBadsNew + NbBadsOld; } }

        [JsonProperty("blague")]
        public string TextNew { get; set; }

        [JsonProperty("pseudo")]
        public string AuthorNew { get; set; }

        [JsonProperty("good")]
        public int NbGoodsNew { get; set; }

        [JsonProperty("bad")]
        public int NbBadsNew { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        [JsonProperty("date_published")]
        public DateTime PublicationDate { get; set; }

        // anciennes versions des proprietes, pour le service de recherche
        [JsonProperty("bl")]
        public string TextOld { get; set; }

        [JsonProperty("a")]
        public string AuthorOld { get; set; }

        [JsonProperty("g")]
        public int NbGoodsOld { get; set; }

        [JsonProperty("b")]
        public int NbBadsOld { get; set; }

    }
}
