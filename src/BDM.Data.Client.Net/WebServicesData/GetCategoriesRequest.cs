using System;
using System.Collections.Generic;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class GetCategoriesRequest : AbstractBaseRequest
    {
        public override string Command
        {
            get { return "categories/get"; }
        }

        public override IEnumerable<KeyValuePair<string, string>> Parameters
        {
            get { return new Dictionary<string, string>(); }
        }
    }
}
