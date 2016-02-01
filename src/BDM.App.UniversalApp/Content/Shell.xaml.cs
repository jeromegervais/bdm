using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.ViewManagement;
using System.Threading.Tasks;
using Windows.UI.Popups;
using BDM.App.UniversalApp.Content.Home;
using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.Utils.UINotifications;
using BDM.App.UniversalApp.Mvvm.Messaging;
using BDM.App.UniversalApp.Utils.Navigation;
using BDM.Common.Model;
using BDM.App.UniversalApp.Content.Categories;
using BDM.App.UniversalApp.Content.Submit;
using BDM.App.UniversalApp.Content.Search;

namespace BDM.App.UniversalApp.Content
{
	public sealed partial class Shell : IPage<ShellViewModel>, IUINotificationService
    {
        public ShellViewModel ViewModel
        {
            get { return DataContext as ShellViewModel; }
            set { DataContext = value; }
        }

        public IReadOnlyList<NavigationMenuItem> MenuItems { get; }

		public Shell()
		{
			InitializeComponent();
            this.InjectViewModel();

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
			if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.HideAsync();
            }

			MenuItems = new List<NavigationMenuItem>
			{
				new NavigationMenuItem {Destination = typeof (HomePage), Label = "Dernières", Order = Order.Last, Icon = '\xE10F'},
				new NavigationMenuItem {Destination = typeof (HomePage), Label = "Aléatoires", Order = Order.Random, Icon = '\xE8B1'},
				new NavigationMenuItem {Destination = typeof (HomePage), Label = "Meilleures", Order = Order.TopWeek, Icon = '\xE8E1'},
                new NavigationMenuItem {Destination = typeof (CategoriesPage), Label = "Catégories", Icon = '\xEA37'},
                new NavigationMenuItem {Destination = typeof (SubmitPage), Label = "Soumettre", Icon = '\xE70F'},
                new NavigationMenuItem {Destination = typeof (SearchPage), Label = "Rechercher", Icon = '\xE11A'},
            };

            Messenger.Current.Subscribe<UINotificationMessage>(OnSendNotificationMessage);
        }

		public void OnLaunched(LaunchActivatedEventArgs launchActivatedEventArgs)
		{
			Navigate(typeof(HomePage));
		}

		public void OnSuspending(SuspendingEventArgs suspendingEventArgs)
		{
			var deferral = suspendingEventArgs.SuspendingOperation.GetDeferral();

			deferral.Complete();
		}

		public void OnResuming()
		{

		}

		private void OnBackRequested(object sender, BackRequestedEventArgs e)
		{
            e.Handled = true;
            if (NavigationFrame.CanGoBack)
				NavigationFrame.GoBack();
            else if (NavigationFrame.Content is HomePage)
                Application.Current.Exit();
            else
                Navigate(typeof(HomePage));
            
        }

		private void OnMenuItemClicked(object sender, RoutedEventArgs e)
		{
			var frameworkElement = sender as FrameworkElement;
			var item = frameworkElement?.DataContext as NavigationMenuItem;
			if (item?.Destination != null)
			{
                if (item?.Order != null)
                    Navigate(item.Destination, item.Order);
                else
                    Navigate(item.Destination);
			}
		}

		private void OnHeaderClicked(object sender, RoutedEventArgs e)
		{
			RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
		}

        public void Navigate(Type destination)
        {
            Navigate(destination, null);
        }

        public void Navigate(Type destination, object parameter)
        {
            RootSplitView.IsPaneOpen = false;
            if (parameter != null)
            {
                //serialisation de la data avant nav
                var navArg = new GlobalNavigationArgs()
                {
                    DataType = parameter.GetType(),
                    Data = parameter
                };

                Guid guid = NavigationMemoryHelper.AddInStack(navArg);

                NavigationFrame.Navigate(destination, guid);
            }
            else
            {
                NavigationFrame.Navigate(destination);
            }
        }

        private void OnSendNotificationMessage(UINotificationMessage m)
        {
            ShowNotificationAsync(m.Message, m.Type);
        }

        public Task ShowNotificationAsync(string message, UINotificationType type = UINotificationType.Info)
        {
            try
            {
                var o = new { Message = message, Type = type };
                ToastContainer.Items.Add(o);

                var tcs = new TaskCompletionSource<object>();

                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                EventHandler<object> func = null;
                func = (sender, args) =>
                {
                    ToastContainer.Items.Remove(o);
                    timer.Stop();
                    timer.Tick -= func;
                    tcs.SetResult(new object());
                };
                timer.Tick += func;
                timer.Start();

                return tcs.Task;
            }
            catch
            {
                // pas de chance. On laisse filer, pas grave !
            }

            return null;
        }

        public async Task<bool> RequestConfirmationAsync(string message, string title = null)
        {
            MessageDialog dialog;
            if (string.IsNullOrWhiteSpace(title))
                dialog = new MessageDialog(message);
            else
                dialog = new MessageDialog(message, title);

            // TODO attention à l'i18n
            dialog.Commands.Add(new UICommand("&Oui"));
            dialog.Commands.Add(new UICommand("&Non"));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 0;

            return await dialog.ShowAsync() == dialog.Commands[0];
        }
    }

	public class NavigationMenuItem
	{
		public string Label { get; set; }

		public char Icon { get; set; }

		public Type Destination { get; set; }

        public Order Order { get; set; }
	}
}
