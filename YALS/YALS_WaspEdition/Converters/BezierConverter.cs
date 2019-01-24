// <copyright file="BezierConverter.cs" company="KW Softworks">
//     Copyright (c) Strommer Kilian. All rights reserved.
// </copyright>
// <summary>Represents the bezier line converter.</summary>
// <author>Killerwasps</author>

namespace YALS_WaspEdition.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// Represents the bezier line converter.
    /// Implements a MultiValueConverter that takes four input coordinates and constructs a horizontal bezier path between them.
    /// </summary>
    public class BezierConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts a line into a smooth bezier path.
        /// </summary>
        /// <param name="values">Int array with the format: X1,Y1,X2,Y2.</param>
        /// <param name="targetType">The target type to convert to.</param>
        /// <param name="parameter">Converter parameters.</param>
        /// <param name="culture">Local culture.</param>
        /// <returns>New bezier geometry.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var coords = values.Select(System.Convert.ToInt32).ToArray();

            // The horizontal offset of the control points.
            const int ControlOffset = 100;

            // M X1,Y1 C X1+100,Y1 X2-100,Y2 X2,Y2
            return Geometry.Parse($"M {coords[0]},{coords[1]} C {coords[0] + ControlOffset},{coords[1]} {coords[2]- ControlOffset},{coords[3]} {coords[2]},{coords[3]}");
        }
   
        /// <summary>
        /// Converts a line into a smooth bezier path.
        /// </summary>
        /// <param name="x1">X Coordinate of the start point.</param>
        /// <param name="y1">Y Coordinate of the start point.</param>
        /// <param name="x2">X Coordinate of the end point.</param>
        /// <param name="y2">Y Coordinate of the end point.</param>
        /// <returns>New bezier geometry.</returns>
        public Geometry Convert(int x1, int y1, int x2, int y2)
        {
            // The horizontal offset of the control points.
            const int ControlOffset = 100;

            return Geometry.Parse($"M {x1},{y1} C {x1 + ControlOffset},{y1} {x2 - ControlOffset},{y2} {x2},{y2}");
        }

        /// <summary>
        /// Unused in this converter.
        /// </summary>
        /// <param name="value">The parameter is not used.</param>
        /// <param name="targetTypes">The parameter is not used.</param>
        /// <param name="parameter">The parameter is not used.</param>
        /// <param name="culture">The parameter is not used.</param>
        /// <returns>Nothing. This is unused.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
