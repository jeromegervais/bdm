using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace BDM.App.UniversalApp.Converters
{
	public class DebugConverter : IValueConverter
	{
		/// <summary>
		/// Converter vide, utilisé pour le debug, afin de connaitre la valeur de l'objet mappé
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, string language)
		{
            return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
