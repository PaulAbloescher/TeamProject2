// <copyright file="SerializableColorToSolidColorBrush.cs" company="KW Softworks">
//     Copyright (c) Paul-Noel Ablöscher. All rights reserved.
// </copyright>
// <summary>Represents a SerializableColorToSolidColorBrush.</summary>
// <author>Killerwasps</author>

namespace YALS_WaspEdition.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using YALS_WaspEdition.GlobalConfig;

    /// <summary>
    /// Represents a SerializableColorToSolidColorBrush.
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class SerializableColorToSolidColorBrush : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (SerializableColor)value;

            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
            return brush;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// Is thrown when the method is called.
        /// </exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
