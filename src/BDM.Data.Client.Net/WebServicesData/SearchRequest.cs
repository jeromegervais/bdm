using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class SearchRequest : AbstractNonGetBaseRequest
    {
        private string _searchWord;

        public SearchRequest(string searchWord)
        {
            _searchWord = searchWord;
        }

        public override string Command => "search";

        public override IEnumerable<KeyValuePair<string, string>> Parameters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "search", _searchWord },
                };
            }
        }
    }
}
