// ----------------------------------------------------------------------- 
// <copyright file="GetColorWithGlobalConfigSettings.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the GetColorWithGlobalConfigSettings class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.GlobalConfig
{
    using System;
    using System.Windows.Media;
    using Shared;

    /// <summary>
    /// Represents the <see cref="GetColorWithGlobalConfigSettings"/> class.
    /// </summary>
    public class GetColorWithGlobalConfigSettings : IGetColorForPin
    {
        /// <summary>
        /// The settings of the class.
        /// </summary>
        private readonly GlobalConfigSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetColorWithGlobalConfigSettings"/> class.
        /// </summary>
        /// <param name="settings">The settings for the class.</param>
        public GetColorWithGlobalConfigSettings(GlobalConfigSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Gets the <see cref="Color"/> instance for an <see cref="IPin"/> item.
        /// </summary>
        /// <param name="item">The item for which the color is determined.</param>
        /// <returns>The <see cref="Color"/> for the item.</returns>
        public Color GetColor(IPin item)
        {
            Color defaultColor = this.ConvertSerializableColorToColor(this.settings.DefaultColor);

            var pinType = item.GetType();
            var pinGenericTypes = pinType.GetGenericArguments();

            if (pinGenericTypes.Length == 0)
            {
                return defaultColor;
            }

            Type type = pinGenericTypes[0];

            if (item.Value == null)
            {
                return defaultColor;
            }

            if (type == typeof(string))
            {
                if (item.Value.Current == null)
                {
                    return defaultColor;
                }

                if (this.settings.StringValues.TryGetValue((string)item.Value.Current, out SerializableColor settingsColor))
                {
                    return this.ConvertSerializableColorToColor(settingsColor);
                }
            }

            if (type == typeof(int))
            {
                if (this.settings.IntValues.TryGetValue((int)item.Value.Current, out SerializableColor settingsColor))
                {
                    return this.ConvertSerializableColorToColor(settingsColor);
                }
            }

            if (type == typeof(bool))
            {
                if (this.settings.BoolValues.TryGetValue((bool)item.Value.Current, out SerializableColor settingsColor))
                {
                    return this.ConvertSerializableColorToColor(settingsColor);
                }
            }

            return defaultColor;
        }

        /// <summary>
        /// Converts an <see cref="SerializableColor"/> instance into a <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The color that gets converted.</param>
        /// <returns>The converted Color.</returns>
        private Color ConvertSerializableColorToColor(SerializableColor color)
        {
            return Color.FromRgb(Convert.ToByte(color.R), Convert.ToByte(color.G), Convert.ToByte(color.B));
        }
    }
}
