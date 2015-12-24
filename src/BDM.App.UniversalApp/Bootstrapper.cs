using System;
using Autofac;
using BDM.App.UniversalApp.Content;
using BDM.App.UniversalApp.Content.Home;
using BDM.App.Shared;
using BDM.Data.Client.Contracts;
using BDM.Data.Client.Net.WebServices;
using BDM.App.UniversalApp.Utils;

namespace BDM.App.UniversalApp
{
    internal class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Shell>().SingleInstance();

            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<ShellViewModel>();

            #region Utils

            builder.RegisterType<AccessIsolatedStorage>().As<IAccessIsolatedStorage>().SingleInstance();
            builder.RegisterType<ApplicationConfiguration>().As<IApplicationConfiguration>().SingleInstance();
            builder.RegisterType<BlaguesHelper>().SingleInstance();
            
            #endregion

            builder.RegisterType<BlaguesService>().As<IBlaguesService>().SingleInstance();

            #region ViewModels

            builder.RegisterType<HomeViewModel>().SingleInstance();
            builder.RegisterType<ShellViewModel>().SingleInstance();

            #endregion
        }
    }
}
