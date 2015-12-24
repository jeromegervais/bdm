using System;
using System.Collections.Generic;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class GetBlaguesRequest : AbstractBaseRequest
    {
        public override string Command
        {
            get { return "blagues/get"; }
        }

        public override IEnumerable<KeyValuePair<string, string>> Parameters
        {
            get { return new Dictionary<string, string>(); }
        }
    }
}
