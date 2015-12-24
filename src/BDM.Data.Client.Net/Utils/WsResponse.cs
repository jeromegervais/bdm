using BDM.Data.Client.Net.WebServicesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Data.Client.Net.Utils
{
    /// <summary>
    /// Classe d'encapsulation de la réponse des WebServices
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class WsResponse<T>
    {
        /// <summary>
        /// Heure de récupération de la donnée.
        /// </summary>
        public DateTime ResponseTime { get; set; }

        /// <summary>
        /// Indique si la donnée provient du cache ou non.
        /// </summary>
        public bool IsFromCache { get; set; } = false;

        /// <summary>
        /// Donnée à proprement parler.
        /// </summary>
        public T Response { get; set; }
    }
}
