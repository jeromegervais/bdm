using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace BDM.App.UniversalApp.Converters
{
    /// <summary>
    /// convertit un booleen en Visibility. Si un <code>parameter</code> est passe, on inverse la valeur
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if ((bool)value)
			{
				return parameter == null ? Visibility.Visible : Visibility.Collapsed;
			}
			return parameter == null ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
