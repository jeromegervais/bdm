using System;
using Windows.UI.Xaml;
using Autofac;
using BDM.App.UniversalApp.Mvvm.ViewModel;

namespace BDM.App.UniversalApp.Utils
{
	public static class InjectionHelper
	{
		private static IContainer _container;

        /// <summary>
        /// A appeler lors de la creation de l'App.
        /// </summary>
        /// <param name="container">Le conteneur de dépendances</param>
		public static void RegisterContainer(IContainer container)
		{
			_container = container;
        }

        /// <summary>
        /// A utiliser pour setter le ViewModel des pages.
        /// Le constructeur de la page doit ressemble à ceci :
        /// public MaPage()
        /// {
        ///     InitializeComponent();
        ///     this.InjectViewModel();
        /// }
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="page"></param>
        public static void InjectViewModel<TViewModel>(this IPage<TViewModel> page) where TViewModel : BasePageViewModel
		{
			page.ViewModel = _container.Resolve<TViewModel>();
		}

		public static void InjectUnsetProperties(this DependencyObject control)
		{
			_container.InjectUnsetProperties(control);
		}
	}
}
