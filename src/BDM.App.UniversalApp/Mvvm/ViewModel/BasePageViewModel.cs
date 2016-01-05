using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.Utils.Navigation;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Navigation;

namespace BDM.App.UniversalApp.Mvvm.ViewModel
{
    public abstract class BasePageViewModel : NotificationObject
    {
        private int _busyCount;

        public bool IsInDesignMode => DesignMode.DesignModeEnabled;
        public virtual bool IsBusy => BusyCount > 0;

        protected virtual bool _hasSharing => false;

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
            SetSharing();

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
            UnsetSharing();
            return Task.FromResult(true);
        }

        public virtual Task OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                NavigationMemoryHelper.Clear(_navigationParam);

            return Task.FromResult(true);
        }

        private void UnsetSharing()
        {
            var dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested -= OnDataSharingRequested;
        }

        public void SetSharing()
        {
            var dataTransferManager = DataTransferManager.GetForCurrentView();

            if (_hasSharing)
            {
                dataTransferManager.DataRequested += OnDataSharingRequested;
            }
            else
            {
                dataTransferManager.DataRequested -= OnDataSharingRequested;
            }
        }

        private void OnDataSharingRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            try
            {
                var sharingObject = OnSharing();
                if (sharingObject == null)
                {
                    args.Request.FailWithDisplayText("Aucun contenu à partager");
                    return;
                }
                var request = args.Request.Data;
                request.Properties.Title = sharingObject.Title;

                if (sharingObject.Text != null)
                    request.SetText(sharingObject.Text);

                if (!String.IsNullOrWhiteSpace(sharingObject.Url))
                    request.SetWebLink(new Uri(sharingObject.Url, UriKind.Absolute));
            }
            catch (Exception ex)
            {
                args.Request.FailWithDisplayText("Aucun contenu à partager");
            }
        }

        public virtual SharingObject OnSharing()
        {
            return null;
        }
    }
}
