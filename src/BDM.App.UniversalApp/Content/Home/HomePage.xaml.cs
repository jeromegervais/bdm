using System;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using Windows.UI.Xaml.Controls;
using BDM.Common.Model;

namespace BDM.App.UniversalApp.Content.Home
{
    public sealed partial class HomePage : IPage<HomeViewModel>
    {
        public HomeViewModel ViewModel
        {
            get { return DataContext as HomeViewModel; }
            set { DataContext = value; }
        }

        public HomePage()
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

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var cmdParam = button.CommandParameter.ToString();
            Order? order = Enum.GetValues(typeof(Order)).Cast<Order>().FirstOrDefault(o => o.ToString() == cmdParam);
            if (order.HasValue)
            {
                ViewModel.SetOrder(order.Value);
            }
        }
    }
}
