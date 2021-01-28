using LQClass.AdminForWPF.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LQClass.AdminForWPF.Infrastructure.Converters
{
	[ValueConversion(typeof(string), typeof(Visibility))]
	public class AuthorizeActionCoverter : IValueConverter
	{
		public object Convert(object value, Type typeTarget, object param, CultureInfo culture)
		{
			var actionUrl = value as string;
			if (actionUrl != null
				&& LoginResultDto.Instance.Attributes.Actions.Exists(cu=>cu.EndsWith(actionUrl)))
			{
				return Visibility.Visible;
			}
			return Visibility.Collapsed;

		}
		public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
		{
			return "";
		}
	}
}
