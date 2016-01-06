using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BDM.App.UniversalApp.Content.Submit
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
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
                await App.Current.GetShell().ShowNotificationAsync("Une erreur est survenue.");
            }
        }
    }
}
