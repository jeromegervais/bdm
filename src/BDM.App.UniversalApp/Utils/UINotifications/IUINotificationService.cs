using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Utils.UINotifications
{

    public interface IUINotificationService
    {
        Task ShowNotificationAsync(string message, UINotificationType type = UINotificationType.Info);
        Task<bool> RequestConfirmationAsync(string message, string title = null);
    }


    public enum UINotificationType : byte
    {
        Info = 0,
        Warning = 1,
        Error = 2,
        Ok = 3
    }
}
