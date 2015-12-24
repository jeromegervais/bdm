using System;
using System.Collections.Generic;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class GetBlaguesForCategoryRequest : AbstractBaseRequest
    {
        private int _categoryId;

        public GetBlaguesForCategoryRequest(int categoryId)
        {
            _categoryId = categoryId;
        }
        public override string Command
        {
            get { return string.Format("category/get/{0}", _categoryId); }
        }

        public override IEnumerable<KeyValuePair<string, string>> Parameters
        {
            get { return new Dictionary<string, string>(); }
        }
    }
}
