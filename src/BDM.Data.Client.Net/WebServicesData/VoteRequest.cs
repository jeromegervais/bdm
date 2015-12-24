using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class VoteRequest : AbstractNonGetBaseRequest
    {
        private int _blagueId;

        private bool _like;

        public VoteRequest(int blagueId, bool like)
        {
            _blagueId = blagueId;
            _like = like;
        }

        public override string Command
        {
            get { return string.Format("{0}/put/{1}", _like ? "good" : "bad", _blagueId); }
        }

        public override IEnumerable<KeyValuePair<string, string>> Parameters
        {
            get { return new Dictionary<string, string>(); }
        }

        public override HttpMethod Method => HttpMethod.Post;

        public override PostDataType PostDataType => PostDataType.Json;
    }
}
