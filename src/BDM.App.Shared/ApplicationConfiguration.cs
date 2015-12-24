using BDM.Data.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BDM.App.Shared
{
    /// <summary>
    /// Classe de gestion de la configuration.
    /// Les infos sont stockees et lues dans le Roaming.
    /// <see cref="https://msdn.microsoft.com/library/windows.storage.applicationdata.roamingsettings.aspx"/>
    /// </summary>
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private Windows.Foundation.Collections.IPropertySet _settings
            => ApplicationData.Current.RoamingSettings.Values;

        public object GetSetting(string name)
        {
            object res;
            return _settings.TryGetValue(name, out res) ? res : null;
        }

        public void SetSetting(string name, object val)
        {
            _settings[name] = val;
        }

        public void DeleteSetting(string name)
        {
            _settings.Remove(name);
        }
    }

}
