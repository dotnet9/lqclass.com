using LQClass.ModuleOfLog.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfLog.Converters
{
	[ValueConversion(typeof(string), typeof(string))]
	public class LogTypeConverter : IValueConverter
	{
		public object Convert(object value, Type typeTarget, object param, CultureInfo culture)
		{
			var logType = value as LogDto;
			if (logType != null
				&& logType.LogType == I18nManager.Instance.Get(I18nResources.Language.Exception).ToString())
			{
				return "Red";
			}
			return "Black";

		}
		public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
		{
			return "";
		}
	}
}
