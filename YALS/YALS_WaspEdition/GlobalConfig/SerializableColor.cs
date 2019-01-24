// ----------------------------------------------------------------------- 
// <copyright file="SerializableColor.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the SerializableColor class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.GlobalConfig
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the <see cref="SerializableColor"/> class.
    /// </summary>
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    [Serializable]
    public class SerializableColor : ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableColor"/> class.
        /// </summary>
        /// <param name="rValue">The red value for the color.</param>
        /// <param name="gValue">The green value for the color.</param>
        /// <param name="bValue">The blue value for the color.</param>
        public SerializableColor(int rValue, int gValue, int bValue)
        {
            if (rValue < 0 || rValue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(rValue));
            }

            if (gValue < 0 || gValue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(gValue));
            }

            if (bValue < 0 || bValue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(bValue));
            }

            this.R = rValue;
            this.G = gValue;
            this.B = bValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableColor"/> class.
        /// </summary>
        /// <param name="info">The information from the serialization.</param>
        /// <param name="context">The context of the streaming.</param>
        public SerializableColor(SerializationInfo info, StreamingContext context)
        {
            this.R = (int)info.GetValue("rValue", typeof(int));
            this.G = (int)info.GetValue("gValue", typeof(int));
            this.B = (int)info.GetValue("bValue", typeof(int));
        }

        /// <summary>
        /// Gets the red value.
        /// </summary>
        /// <value>
        /// The red value.
        /// </value>
        public int R
        {
            get;
        }

        /// <summary>
        /// Gets the green value.
        /// </summary>
        /// <value>
        /// The green value.
        /// </value>
        public int G
        {
            get;
        }

        /// <summary>
        /// Gets the blue value.
        /// </summary>
        /// <value>
        /// The blue value.
        /// </value>
        public int B
        {
            get;
        }

        /// <summary>
        /// Populates a <see cref="SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("rValue", this.R);
            info.AddValue("gValue", this.G);
            info.AddValue("bValue", this.B);
        }
    }
}
