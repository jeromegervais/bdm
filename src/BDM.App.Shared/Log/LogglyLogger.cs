using System.Collections.Generic;
using System.Net.Http;

namespace BDM.App.Shared.Log
{
    internal class LogglyConfig
    {
        
    }

    /// <summary>
    /// Logger utilisant l'API Rest de Loggly.
    /// </summary>
    /// <see cref="https://www.loggly.com/docs/api-overview/"/>
    /// <typeparam name="T"></typeparam>
    internal class LogglyLogger : Logger
    {
        private readonly string _token;
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="token">le token de connexion</param>
        public LogglyLogger(string token)
        {
            _token = token;
        }

        protected override void Write(LogLevel logLevel, IEnumerable<KeyValuePair<string, string>> data)
        {
            try
            {
                if (logLevel < LogManager.MinLevel)
                    return;

                using (var client = new HttpClient())
                {
                    var requestMessage = new HttpRequestMessage(
                        HttpMethod.Post,
                        string.Format("http://logs-01.loggly.com/inputs/{0}/tag/http/", _token));
                    requestMessage.Content = new FormUrlEncodedContent(GetParams(logLevel, data));

                    var r = client.SendAsync(requestMessage).Result;
                    r = null;
                }
            }
            catch
            {
                // pas de chance !
            }
        }
    }
}
