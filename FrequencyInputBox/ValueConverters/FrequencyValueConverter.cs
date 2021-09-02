using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrequencyInputBox.ValueConverters
{
    internal class FrequencyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d=0;

            if (value is string)
                double.TryParse((string)value, out d);
            
            return d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
