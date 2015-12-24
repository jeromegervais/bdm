using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.Utils.Navigation;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Navigation;

namespace BDM.App.UniversalApp.Mvvm.ViewModel
{
    public abstract class BasePageViewModel : NotificationObject
    {
        private int _busyCount;

        public bool IsInDesignMode => DesignMode.DesignModeEnabled;
        public virtual bool IsBusy => BusyCount > 0;

        public virtual int BusyCount
        {
            get { return _busyCount; }
            set
            {
                if (_busyCount != value)
                {
                    _busyCount = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(IsBusy));
                }
            }
        }

        private Guid _navigationParam;

        public Task OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Guid)
                _navigationParam = (Guid)e.Parameter;

            var arg = OwnNavigationEventArgs.FromNavigationArgs(e);
            OnNavigatedTo(arg);

            return Task.FromResult(true);
        }

        public virtual Task OnNavigatedTo(OwnNavigationEventArgs e)
        {
            return Task.FromResult(true);
        }

        public virtual Task OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            return Task.FromResult(true);
        }

        public virtual Task OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                NavigationMemoryHelper.Clear(_navigationParam);

            return Task.FromResult(true);
        }
    }
}
