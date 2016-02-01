using BDM.App.UniversalApp.Content.Home;
using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using BDM.Common.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BDM.App.UniversalApp.Content.Categories
{
    public sealed partial class CategoriesPage : IPage<CategoriesViewModel>
    {
        public CategoriesViewModel ViewModel
        {
            get { return DataContext as CategoriesViewModel; }
            set { DataContext = value; }
        }

        public CategoriesPage()
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

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Category category = (sender as Border)?.DataContext as Category;
            if (category != null)
            {
                App.Current.GetShell().Navigate(typeof(CategoryPage), category);
            }
        }
    }
}
