using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Utils.UINotifications
{
    /// <summary>
    /// Classe de gestion des messages Toast à afficher à l'utilisateur
    /// </summary>
    public class UINotificationMessage
    {
        public string Message { get; private set; }

        public UINotificationType Type { get; private set; }

        public UINotificationMessage(string message, UINotificationType type)
        {
            Message = message;
            Type = type;
        }
    }
}
