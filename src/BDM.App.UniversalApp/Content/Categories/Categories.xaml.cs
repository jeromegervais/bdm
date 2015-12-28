using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using BDM.Common.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BDM.App.UniversalApp.Content.Categories
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Category category = (sender as Grid)?.DataContext as Category;
            if (category != null)
            {

            }
        }
    }
}
