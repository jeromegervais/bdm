using BDM.App.UniversalApp.Content;
using Windows.UI.Xaml;

namespace BDM.App.UniversalApp.Utils
{
    public static class AppExtensions
    {
        public static Shell GetShell(this Application application)
        {
            return ((App)application).Shell;
        }

        public static ShellViewModel GetShellViewModel(this Application application)
        {
            return (((App)application).Shell.DataContext as ShellViewModel);
        }

        public static BlaguesHelper GetBlaguesHelper(this Application application)
        {
            return ((App)application).BlaguesHelper;
        }
    }
}
