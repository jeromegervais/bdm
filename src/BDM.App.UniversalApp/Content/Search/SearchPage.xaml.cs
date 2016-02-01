using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BDM.App.UniversalApp.Content.Search
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SearchPage : IPage<SearchViewModel>
    {
        public SearchViewModel ViewModel
        {
            get { return DataContext as SearchViewModel; }
            set { DataContext = value; }
        }

        public SearchPage()
        {
            this.InitializeComponent();
            this.InjectViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.OnNavigatedTo(e);
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            await ViewModel.OnNavigatingFrom(e);
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            await ViewModel.OnNavigatedFrom(e);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SearchWord.Length > 0)
            {
                await ViewModel.Search();
            }
            else
            {
                await App.Current.GetShell().ShowNotificationAsync("Tu dois remplir le champ de recherche.", Utils.UINotifications.UINotificationType.Warning);
            }
        }

        private void Vote_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var cmdParam = button.CommandParameter.ToString();

            ViewModel.Vote(button.DataContext as BlagueVM, cmdParam == "Like");
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ViewModel.SetSharingObject(button.DataContext as BlagueVM);
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }
    }
}
