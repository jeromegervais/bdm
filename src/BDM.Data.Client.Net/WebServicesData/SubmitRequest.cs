using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Data.Client.Net.WebServicesData
{
    internal class SubmitRequest : AbstractNonGetBaseRequest
    {
        private string _blague;

        private string _pseudo;

        private string _email;

        public SubmitRequest(string blague, string pseudo, string email)
        {
            _blague = blague;
            _pseudo = pseudo;
            _email = email;
        }

        public override string Command
        {
            get { return "post?source=8"; }
        }

        public override IEnumerable<KeyValuePair<string, string>> Parameters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "blague", _blague },
                    { "pseudo", _pseudo },
                    { "email", _email ?? "no-reply@blaguesdemerde.fr" }
                };
            }
        }

        public override HttpMethod Method => HttpMethod.Post;

        public override PostDataType PostDataType => PostDataType.KeyValue;

        /// <summary>
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public override HttpRequestMessage GetHttpRequestMessage(string uri)
        {
            string fullUrl = uri + (uri.Contains("?") ? (uri.EndsWith("?") ? "" : "&") : "?") + "source=5";
            var requestMessage = new HttpRequestMessage(Method, fullUrl);
            return requestMessage;
        }
    }
}
