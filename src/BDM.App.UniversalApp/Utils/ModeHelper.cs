using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.System.Profile;
using Windows.UI.ViewManagement;

namespace BDM.App.UniversalApp.Utils
{
    public static class ModeHelper
    {
		public const double MIN_WIDTH_PAYSAGE = 800;
		
        public static event EventHandler OnModeChangedEvent;
        public static event EventHandler OnInputModeChangedEvent;

        public static Mode CurrentMode =>
            ApplicationView.GetForCurrentView().VisibleBounds.Width > MIN_WIDTH_PAYSAGE ? Mode.Wide : Mode.Tall;

        public static InputMode CurrentInputMode =>
            UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Mouse ? InputMode.Mouse : InputMode.Touch;

        /// <summary>
        /// Permet de connaitre le type de device sur lequel est exécutée l'application
        /// </summary>
        public static DeviceType Device
        {
            get
            {
                if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                {
                    return DeviceType.Smartphone;
                }
                return (CurrentInputMode == InputMode.Touch) ? DeviceType.Tablette : DeviceType.Desktop;
            }
        }

        /// <summary>
        /// Permet de connaitre l'orientation du device sur lequel est exécutée l'application
        /// </summary>
        public static DeviceOrientation Orientation =>
            DisplayInformation.GetForCurrentView().CurrentOrientation.ToString().Contains("Landscape") ? DeviceOrientation.Paysage : DeviceOrientation.Portrait;


        private static Mode _previousMode;
        private static InputMode _previousInputMode;

        /// <summary>
        /// Methode de gestion du changement de taille de l'application.
        /// TODO a utiliser s'il y a un design adaptatif en fonction de la taille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            if (_previousMode != CurrentMode)
            {
                OnModeChangedEvent?.Invoke(null, null);
                _previousMode = CurrentMode;
            }
            if (_previousInputMode != CurrentInputMode)
            {
                OnInputModeChangedEvent?.Invoke(null, null);
                _previousInputMode = CurrentInputMode;
            }
        }
    }

    // TODO ajouter les eventuels autres modes
    public enum Mode
    {
        Wide,
        Tall
    }

    public enum InputMode
    {
        Touch,
        Mouse
    }

    public enum DeviceType
    {
        Tablette,
        Smartphone,
        Desktop
    }

    public enum DeviceOrientation
    {
        Paysage,
        Portrait
    }
}
