using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using static Cibc.PriceService.Prices;

namespace Cibc.UI.View
{
    public class ValueToColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is PriceChangeDirection direction)
            {
                switch (direction)
                {
                    case PriceChangeDirection.Up:
                        return Brushes.Green;
                    case PriceChangeDirection.Down:
                        return Brushes.Red;
                    default:
                        return Brushes.White;
                }
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
