using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;

namespace BDM.Data.Client.Net.WebServicesData
{
    /// <summary>
    /// Classe de base pour toutes les requetes envoyées via WebService
    /// </summary>
    internal abstract class AbstractBaseRequest
	{
		public abstract string Command { get; }

        public abstract IEnumerable<KeyValuePair<string, string>> Parameters { get; }

        public virtual HttpMethod Method
            => HttpMethod.Get;

        public virtual DecompressionMethods? AutomaticDecompression
            => DecompressionMethods.Deflate | DecompressionMethods.GZip;

        public virtual Action<HttpRequestHeaders> ApplyToHeaders { get; }

        private const string ClientId = "kh9nhAkrff";
        /// <summary>
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public virtual HttpRequestMessage GetHttpRequestMessage(string uri)
        {
            string fullUrl = uri + (uri.Contains("?") ? (uri.EndsWith("?") ? "" : "&") : "?") + "client_id=" + ClientId;
            var requestMessage = new HttpRequestMessage(Method, fullUrl);
            return requestMessage;
        }
    }

    /// <summary>
    /// Classe de base pour les requetes en POST et en PUT
    /// </summary>
	internal abstract class AbstractNonGetBaseRequest : AbstractBaseRequest
	{
        public virtual PostDataType PostDataType { get; }
	}

	public enum PostDataType
	{
        /// <summary>
        /// La requete doit etre envoyée au format keyvaluePair
        /// </summary>
		KeyValue,

        /// <summary>
        /// La requete doit etre envoyée au format Json
        /// </summary>
		Json
	}
}
