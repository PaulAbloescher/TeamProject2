// <copyright file="BezierConverter.cs" company="KW Softworks">
//     Copyright (c) Paul-Noel Ablöscher. All rights reserved.
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
    /// </summary>
    public class BezierConverter : IMultiValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var coords = values.Select(System.Convert.ToInt32).ToArray();

            const int controlOffset = 100;
            // M X1,Y1 C X1+100,Y1 X2-100,Y2 X2,Y2
            return Geometry.Parse($"M {coords[0]},{coords[1]} C {coords[0] + controlOffset},{coords[1]} {coords[2]- controlOffset},{coords[3]} {coords[2]},{coords[3]}");
        }


        public Geometry Convert(int x1, int y1, int x2, int y2)
        {
            const int controlOffset = 100;

            return Geometry.Parse($"M {x1},{y1} C {x1 + controlOffset},{y1} {x2 - controlOffset},{y2} {x2},{y2}");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
