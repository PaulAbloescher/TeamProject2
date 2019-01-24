// <copyright file="INode.cs" company="KW Softworks">
//     Copyright (c) Strommer Kilian. All rights reserved.
// </copyright>
// <summary>Represents the bezier line converter.</summary>
// <author>Killerwasps</author>
namespace Shared
{
    using System.Collections.Generic;

    /// <summary>
    /// Specifies the interface for Nodes (Components).
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// Gets the inputs of the node.
        /// </summary>
        /// <value>A list of IPin instances.</value>
        ICollection<IPin> Inputs { get; }

        /// <summary>
        /// Gets the outputs of the node.
        /// </summary>
        /// <value>A list of IPin instances.</value>
        ICollection<IPin> Outputs { get; }

        /// <summary>
        /// Gets the label text of the node.
        /// </summary>
        /// <value>String containing the label text of the node.</value>
        string Label { get; }

        /// <summary>
        /// Gets the description of the node.
        /// </summary>
        /// <value>String containing the description.</value>
        string Description { get; }

        /// <summary>
        /// Gets the NodeType of the node.
        /// </summary>
        /// <value>NodeType representing the type of the node.</value>
        NodeType Type { get; }

        /// <summary>
        /// Called when this node ticks.
        /// </summary>
        void Execute();
        
        /// <summary>
        /// Called when the user activates the node.
        /// </summary>
        void Activate();
    }
}
