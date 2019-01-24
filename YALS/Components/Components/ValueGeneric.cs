// ---------------------------------------------------------------------
// <copyright file="ValueGeneric.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>Class that is used in order to make the types of the pins of the components generic.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using Shared;

    /// <summary>
    /// Class that is used in order to make the types of the pins of the components generic.
    /// </summary>
    /// <typeparam name="T">The type of the value of the pin.</typeparam>
    /// <seealso cref="Shared.IValueGeneric{T}" />
    [Serializable]
    public class ValueGeneric<T> : IValueGeneric<T>
    {
        /// <summary>
        /// Gets or sets the current value of the pin.
        /// </summary>
        /// <value>
        /// The current value of the pin..
        /// </value>
        public T Current
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current value of the pin.
        /// </summary>
        /// <value>
        /// The current value of the pin.
        /// </value>
        object IValue.Current
        {
            get
            {
                return this.Current;
            }

            set
            {
                this.Current = (T)value;
            }
        }
    }
}
