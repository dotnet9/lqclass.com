using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using LQClass.AdminForWPF.Infrastructure.Models;

namespace LQClass.AdminForWPF.Infrastructure.Converters;

[ValueConversion(typeof(string), typeof(Visibility))]
public class AuthorizeActionCoverter : IValueConverter
{
    public object Convert(object value, Type typeTarget, object param, CultureInfo culture)
    {
        var actionUrl = value as string;
        if (actionUrl != null
            && LoginResultDto.Instance.Attributes.Actions.Exists(cu => cu.EndsWith(actionUrl)))
            return Visibility.Visible;
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
    {
        return "";
    }
}