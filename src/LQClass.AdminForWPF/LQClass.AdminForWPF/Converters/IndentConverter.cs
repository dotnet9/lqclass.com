﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using LQClass.AdminForWPF.Infrastructure.Models;

namespace LQClass.AdminForWPF.Converters;

public class IndentConverter : IValueConverter
{
    public double Indent { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var item = value as TreeViewItem;
        if (null == item) return new Thickness(0);

        var menu = item.DataContext as CustomMenuItem;
        return new Thickness((Indent - 1) * menu.Level, 5, 0, 0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}