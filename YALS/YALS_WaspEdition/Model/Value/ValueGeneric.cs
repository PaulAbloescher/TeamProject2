// ----------------------------------------------------------------------- 
// <copyright file="ValueGeneric.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the ValueGeneric class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.Model.Component.Value
{
    using Shared;

    /// <summary>
    /// Represents the <see cref="ValueGeneric{T}"/> class.
    /// </summary>
    /// <typeparam name="T">The type for the generic value.</typeparam>
    public class ValueGeneric<T> : IValueGeneric<T>
    {
        /// <summary>
        /// Gets or sets the current <see cref="T"/> value.
        /// </summary>
        /// <value>
        /// The current <see cref="T"/> value.
        /// </value>
        public T Current
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current value object.
        /// </summary>
        /// <value>
        /// The current value object.
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
