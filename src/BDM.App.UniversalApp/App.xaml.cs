using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Autofac;
using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.Content;
using BDM.App.Shared.Log;
using System.Reflection;

namespace BDM.App.UniversalApp
{
	sealed partial class App
	{
		private readonly IContainer _container;
		public Shell Shell => _container.Resolve<Shell>();

        public BlaguesHelper BlaguesHelper => _container.Resolve<BlaguesHelper>();

        public ILogger Logger { get ; }

        public App()
		{
			InitializeComponent();
			_container = CreateContainer();
			InjectionHelper.RegisterContainer(_container);
			Suspending += OnSuspending;
			Resuming += OnResuming;

            LogManager.AssemblyName = typeof(Application).GetTypeInfo().Assembly.FullName;
            Logger = LogManager.CreateLogglyLogger("1eeac402-998a-46ee-b87f-7781c6ee92b6");
        }

		private IContainer CreateContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<Bootstrapper>();
			return builder.Build();
		}

		protected override void OnWindowCreated(WindowCreatedEventArgs args)
		{
			if (args.Window.Content != Shell)
				args.Window.Content = Shell;

			args.Window.Activate();
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
			if (e.PreviousExecutionState != ApplicationExecutionState.Running)
			{
				Shell.OnLaunched(e);
			}
		}

		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			Shell.OnSuspending(e);
		}

		private void OnResuming(object sender, object e)
		{
			Shell.OnResuming();
		}
	}
}
