using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace BDM.App.UniversalApp.Content.Submit
{

    public sealed partial class SubmitPage : IPage<SubmitViewModel>
	{
        public SubmitViewModel ViewModel
        {
            get { return DataContext as SubmitViewModel; }
            set { DataContext = value; }
        }

        public SubmitPage()
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
            if (string.IsNullOrEmpty(ViewModel.Email) || string.IsNullOrEmpty(ViewModel.Blague))
            {
                await App.Current.GetShell().ShowNotificationAsync("Tu dois remplir ton pseudo et ta blague.", Utils.UINotifications.UINotificationType.Warning);
            }
            else if (await ViewModel.Submit())
            {
                await App.Current.GetShell().ShowNotificationAsync("Ta blague a bien été soumise.");
            }
            else
            {
                await App.Current.GetShell().ShowNotificationAsync("Une erreur est survenue.", Utils.UINotifications.UINotificationType.Error);
            }
        }
    }
}
