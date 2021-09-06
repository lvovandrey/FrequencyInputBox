using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace FrequencyInputControl.Converters
{
    internal class ValidityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush color = new SolidColorBrush(Colors.Red);
            if (value != null && value is bool && (bool)value)
            {
                color = new SolidColorBrush(Colors.Green);
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
