using System;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using Windows.UI.Xaml.Controls;
using BDM.Common.Model;
using Windows.UI.Xaml;
using BDM.App.UniversalApp.ViewModels;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace BDM.App.UniversalApp.Content.Categories
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CategoryPage : IPage<CategoryPageViewModel>
    {
        public CategoryPageViewModel ViewModel
        {
            get { return DataContext as CategoryPageViewModel; }
            set { DataContext = value; }
        }

        public CategoryPage()
        {
            InitializeComponent();
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

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.ReloadBlagues();
        }
    }
}
