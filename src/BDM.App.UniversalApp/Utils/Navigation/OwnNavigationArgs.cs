using System;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace BDM.App.UniversalApp.Utils.Navigation
{
    /// <summary>
    /// Version custom du NavigationEventArgs pour qui le parameter est sérialisé.
    /// </summary>
    public class OwnNavigationEventArgs
    {
        public Object Content { get; set; }

        public NavigationMode NavigationMode { get; set; }

        public NavigationTransitionInfo NavigationTransitionInfo { get; set; }

        public Object Parameter { get; set; }

        public Type SourcePageType { get; set; }

        public Uri Uri { get; set; }

        /// <summary>
        /// Crée une instance à partir d'un NavigationEventArgs.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static OwnNavigationEventArgs FromNavigationArgs(NavigationEventArgs args)
        {
            return new OwnNavigationEventArgs
            {
                Content = args.Content,
                NavigationMode = args.NavigationMode,
                NavigationTransitionInfo = args.NavigationTransitionInfo,
                Parameter = DeserializeData(args),
                SourcePageType = args.SourcePageType,
                Uri = args.Uri
            };
        }


        private static object DeserializeData(NavigationEventArgs arg)
        {
            try
            {
                // si l'objet de nav n'avait pas été sérialisé, on renvoie l'objet associé
                if (arg.Parameter is GlobalNavigationArgs)
                    return (arg.Parameter as GlobalNavigationArgs).Data;
                if (arg.Parameter is Guid)
                {
                    var param = NavigationMemoryHelper.Get((Guid)arg.Parameter);

                    if (param.Data != null)
                        return param.Data;
                    if (!String.IsNullOrEmpty(param.SerializedData))
                    {
                        GlobalNavigationArgs result = param.SerializedData.GenericDeserialize(typeof(GlobalNavigationArgs)) as GlobalNavigationArgs;
                        return result.SerializedData.GenericDeserialize(result.DataType);
                    }
                }
            }
            catch { }
            return arg.Parameter;
        }
    }
}
