using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using YALS_WaspEdition.GlobalConfig;

namespace YALS_WaspEdition.Converters
{
    public class SerializableColorToSolidColorBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (SerializableColor)value;

            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
