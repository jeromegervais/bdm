using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Data.Client.Contracts
{
    /// <summary>
    /// Interface à implémenter pour gérer le stockage des informations de configuration de l'application.
    /// </summary>
    public interface IApplicationConfiguration
    {
        object GetSetting(string name);
        void SetSetting(string name, object val);
        void DeleteSetting(string name);
    }
}
