﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LQClass.ModuleOfMenuManagement.Converters
{
    [ValueConversion(typeof(string), typeof(bool))]

    public class ColumnBoolConverters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool dataval = false;
            bool.TryParse(value.ToString(), out dataval);
            return dataval;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
