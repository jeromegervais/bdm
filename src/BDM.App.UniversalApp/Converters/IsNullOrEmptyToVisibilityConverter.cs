using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace BDM.App.UniversalApp.Converters
{
    /// <summary>
    /// Convertit un <code>Object</code> en Visibility.
    /// </summary>
    public class IsNullOrEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!string.IsNullOrWhiteSpace(value as string) || value != null)
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
