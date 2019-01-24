// <copyright file="NodeType.cs" company="KW Softworks">
//     Copyright (c) Strommer Kilian. All rights reserved.
// </copyright>
// <summary>Represents the bezier line converter.</summary>
// <author>Killerwasps</author>
namespace Shared
{
    /// <summary>
    /// Defines the different node types.
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// Logic gates.
        /// </summary>
        Logic,

        /// <summary>
        /// Output nodes, LEDs and such.
        /// </summary>
        Display,

        /// <summary>
        /// Input nodes.
        /// </summary>
        Source,

        /// <summary>
        /// Interactive nodes.
        /// </summary>
        Switch,

        /// <summary>
        /// Converter nodes.
        /// </summary>
        Converter
    }
}
