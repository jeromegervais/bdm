using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BDM.Data.Client.Contracts;
using BDM.Data.Client.Net.WebServicesData;
using Newtonsoft.Json;

namespace BDM.Data.Client.Net.Utils
{
    public enum CachePolicy
    {
        /// <summary>
        /// on utilise le cache si celui ci est suffisamment récent
        /// </summary>
        OnlyFreshValues,
        /// <summary>
        /// On n'utilise jamais le cache
        /// </summary>
        NoUse,
        /// <summary>
        /// Si on n'arrive pas à recuperer de données fraiches, on renvoie celles du cache, meme expirées
        /// </summary>
        CanUseOldValues
    }

    /// <summary>
    /// Classe d'appel des webservices.
    /// </summary>
    internal class RestClient
    {
        public TimeSpan Timeout { get; set; }
        private readonly IAccessIsolatedStorage _accesIsolatedStorage;

        public RestClient(IAccessIsolatedStorage isolatedStorage)
        {
            _accesIsolatedStorage = isolatedStorage;
            Timeout = TimeSpan.FromSeconds(60);
        }

        private HttpRequestMessage PrepareRequestMessage(AbstractBaseRequest request, string uri)
        {
            var requestMessage = request.GetHttpRequestMessage(uri);
            return requestMessage;
        }

        public static readonly TimeSpan DefaultCacheLifetime = TimeSpan.FromMinutes(10);
        public static readonly TimeSpan NoCache = TimeSpan.Zero;
        public static readonly TimeSpan InfiniteCache = TimeSpan.FromDays(30);
        public static readonly TimeSpan LongCache = TimeSpan.FromDays(1);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="baseUrl"></param>
        /// <param name="path"></param>
        /// <param name="req"></param>
        /// <param name="cacheLifetime">la durée de cache pour cette requette</param>
        /// <param name="cachePolicy">la politique de cache (optionnel)</param>
        /// <returns>L'objet dé-serialisé obtenu en interrogeant l'url demandée, encapsulé dans une <code>WsResponse</code></returns>
        public async Task<WsResponse<TResult>> SendDataAsync<TData, TResult>(string baseUrl,
            string path,
            TData req,
            TimeSpan cacheLifetime,
            CachePolicy cachePolicy = CachePolicy.CanUseOldValues)
            where TData : AbstractBaseRequest
        {
            try
            {
                var url = string.Format("{0}/{1}", baseUrl, path);

                using (var handler = new HttpClientHandler())
                {
                    if (req.AutomaticDecompression != null)
                    {
                        handler.AutomaticDecompression = req.AutomaticDecompression.Value;
                    }
                    using (var client = new HttpClient(handler))
                    {
                        req.ApplyToHeaders?.Invoke(client.DefaultRequestHeaders);

                        if (req.Method == HttpMethod.Get)
                            return await GetDataAsync<TData, TResult>(url, client, req, cacheLifetime, cachePolicy);
                        else
                            return await PostDataAsync<TData, TResult>(url, client, req);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new WsException(new Guid(), DecriptCode(ex.Message), ex.Message, WsErrors.TechnicalError);
            }
        }

        /// <summary>
        /// Gestion des appels HTTP Get
        /// </summary>
        private async Task<WsResponse<TResult>> GetDataAsync<TData, TResult>(string url,
            HttpClient client,
            TData req,
            TimeSpan cacheLifetime,
            CachePolicy cachePolicy)
            where TData : AbstractBaseRequest
        {
            WsResponse<TResult> response = new WsResponse<TResult>();

            string stringQuery = "?";
            if (req.Parameters != null && req.Parameters.Any())
            {
                stringQuery += string.Join("&", req.Parameters
                                                    .Where(parameter => !string.IsNullOrWhiteSpace(parameter.Value))
                                                    .Select(parameter => parameter.Key + "=" + parameter.Value));
            }

            string fullUrl = url + stringQuery;
            string cacheFileName = GetCacheFileName(fullUrl);
            string stringResult = null;
            TimeSpan cacheAge = TimeSpan.MaxValue;

            bool isDataFromCache = false;
            bool mustWriteInCache = false;

            // si on veut utiliser le cache, et que celui ci est disponible...
            if (cachePolicy != CachePolicy.NoUse && _accesIsolatedStorage != null)
            {
                DateTimeOffset? offset = await _accesIsolatedStorage.GetModificationDateAsync(cacheFileName);

                if (offset != null)
                {
                    cacheAge = DateTimeOffset.Now - offset.Value;

                    // si le cache est recent ou si on autorise les données expirées, on lit la valeur
                    if (cacheAge < cacheLifetime || cachePolicy == CachePolicy.CanUseOldValues)
                    {
                        stringResult = await _accesIsolatedStorage.ReadIsolatedStorageAsync(cacheFileName);
                        isDataFromCache = true;
                        response.ResponseTime = offset.Value.DateTime;
                        response.IsFromCache = true;
                    }
                }
            }

            // si pas de données en cache, ou si cache expiré
            if (cacheAge > cacheLifetime)
            {
                using (var requestMessage = PrepareRequestMessage(req, fullUrl))
                {
#if DEBUG || INT		
                    Debug.WriteLine("REST : " + fullUrl);
#endif
                    try
                    {
                        using (var result = await client.SendAsync(requestMessage))
                        {
                            result.EnsureSuccessStatusCode();
                            string tmpStringResult = await result.Content.ReadAsStringAsync();
                            if (tmpStringResult != null)
                            {
                                stringResult = tmpStringResult;
                                response.ResponseTime = DateTime.Now;
                                if (_accesIsolatedStorage != null && cachePolicy != CachePolicy.NoUse)
                                {
                                    mustWriteInCache = true;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // si on autorise les données expirées et que l'on a récupéré des données en cache,
                        // on ne throw pas d'exception en cas de d'erreur lors de la requete
                        if (!(stringResult != null && cachePolicy == CachePolicy.CanUseOldValues))
                        {
                            throw;
                        }
                    }
                }
            }
            else
            {
#if DEBUG
                Debug.WriteLine("CACHE : " + url + stringQuery);
#endif
            }

            try
            {
                TResult res = Deserialize<TResult>(stringResult);
                response.Response = res;
                if (mustWriteInCache)
                {
                    _accesIsolatedStorage.WriteIsolatedStorageAsync(cacheFileName, stringResult);
                }
                return response;
            }
            catch (Exception ex)
            {
                // si la donnée provient du cache et qu'on n'arrive pas à deserialiser: on supprime le cache
                if (isDataFromCache)
                {
                    try { _accesIsolatedStorage.DeleteFileAsync(cacheFileName); }
                    catch { } // vraiment pas de chance !
                }
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullUrl"></param>
        /// <returns>Le nom du fichier de cache correspondant a l'url demandée</returns>
        private string GetCacheFileName(string fullUrl)
        {
            return string.Concat(Encoding.UTF8.GetBytes(fullUrl).ToArray().Select(b => b.ToString("X2")));
        }

        /// <summary>
        /// Gestion des appels POST
        /// </summary>
        private async Task<WsResponse<TResult>> PostDataAsync<TData, TResult>(string url,
            HttpClient client,
            TData req)
            where TData : AbstractBaseRequest
        {
            string stringResult = null;

            using (var requestMessage = PrepareRequestMessage(req, url))
            {
                AbstractNonGetBaseRequest postReq = req as AbstractNonGetBaseRequest;
                if (postReq == null || postReq.PostDataType == PostDataType.Json)
                {
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(req.Parameters))
                    {
                        Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                    };
                }
                else
                {
                    requestMessage.Content = new FormUrlEncodedContent(req.Parameters);
                }

                using (var result = await client.SendAsync(requestMessage))
                {
                    result.EnsureSuccessStatusCode();
                    stringResult = await result.Content.ReadAsStringAsync();
                }
            }
            
            TResult res = Deserialize<TResult>(stringResult);
            return new WsResponse<TResult>() { ResponseTime = DateTime.Now, Response = res };
        }

        /// <summary>
        /// Deserialise le string recu du WebService.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="stringResult">la chaine de caractères recue du WebService</param>
        /// <returns>La version Objet de la chaine de caractères recue du WebService</returns>
        private TResult Deserialize<TResult>(string stringResult)
        {
            return JsonConvert.DeserializeObject<TResult>(stringResult);
        }

        private static readonly string[] Codes = { "200", "400", "401", "403", "404", "405", "406", "500", "503" };

        public HttpReturnCode DecriptCode(string exception)
        {
            var code = Codes.FirstOrDefault(exception.Contains) ?? string.Empty;

            switch (code)
            {
                case "200":
                    return HttpReturnCode.Code200;
                case "400":
                    return HttpReturnCode.Code400;
                case "401":
                    return HttpReturnCode.Code401;
                case "403":
                    return HttpReturnCode.Code403;
                case "404":
                    return HttpReturnCode.Code404;
                case "405":
                    return HttpReturnCode.Code405;
                case "406":
                    return HttpReturnCode.Code406;
                case "500":
                    return HttpReturnCode.Code500;
                case "503":
                    return HttpReturnCode.Code503;
                default:
                    return HttpReturnCode.Code200;
            }
        }

    }
}
