// ----------------------------------------------------------------------- 
// <copyright file="GlobalConfigSettings.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the GlobalConfigSettings class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------

namespace YALS_WaspEdition.GlobalConfig
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the <see cref="GlobalConfigSettings"/> class.
    /// </summary>
    [Serializable]
    public class GlobalConfigSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalConfigSettings"/> class.
        /// </summary>
        public GlobalConfigSettings()
        {
            this.IntValues = new Dictionary<int, SerializableColor>();
            this.BoolValues = new Dictionary<bool, SerializableColor>();
            this.StringValues = new Dictionary<string, SerializableColor>();
        }

        /// <summary>
        /// Gets or sets the default color for the settings.
        /// </summary>
        /// <value>
        /// The default color for the settings.
        /// </value>
        public SerializableColor DefaultColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the integer values with the corresponding color values.
        /// </summary>
        /// <value>
        /// The integer values with the corresponding color values.
        /// </value>
        public Dictionary<int, SerializableColor> IntValues
        {
            get;
        }

        /// <summary>
        /// Gets the boolean values with the corresponding color values.
        /// </summary>
        /// <value>
        /// The boolean values with the corresponding color values.
        /// </value>
        public Dictionary<bool, SerializableColor> BoolValues
        {
            get;
        }

        /// <summary>
        /// Gets the string values with the corresponding color values.
        /// </summary>
        /// <value>
        /// The string with the corresponding color values.
        /// </value>
        public Dictionary<string, SerializableColor> StringValues
        {
            get;
        }

        /// <summary>
        /// Gets default settings where there are already the two boolean settings and a default color.
        /// </summary>
        /// <returns>A <see cref="GlobalConfigSettings"/>Instance with default values.</returns>
        public static GlobalConfigSettings GetDefaultSettings()
        {
            GlobalConfigSettings settings = new GlobalConfigSettings();
            settings.BoolValues.Add(true, new SerializableColor(0, 0, 255));
            settings.BoolValues.Add(false, new SerializableColor(255, 0, 0));
            settings.DefaultColor = new SerializableColor(0, 0, 0);
            return settings;
        }
    }
}
