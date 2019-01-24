// ----------------------------------------------------------------------- 
// <copyright file="CurrentState.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the CurrentState class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.Model.Serialization
{
    using System;
    using System.Collections.Generic;
    using YALS_WaspEdition.GlobalConfig;
    using YALS_WaspEdition.ViewModels;

    /// <summary>
    /// Represents the <see cref="CurrentState"/> class.
    /// </summary>
    [Serializable]
    public class CurrentState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentState"/> class.
        /// </summary>
        /// <param name="settings">The settings of the current state.</param>
        /// <param name="connectedPins">The connected pins of the current state.</param>
        /// <param name="nodeVMs">The <see cref="NodeVM"/> instances of the current state.</param>
        public CurrentState(GlobalConfigSettings settings, ICollection<Tuple<PinVM, PinVM>> connectedPins, ICollection<NodeVM> nodeVMs)
        {
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.ConnectedPins = connectedPins ?? throw new ArgumentNullException(nameof(connectedPins));
            this.NodeVMsWithoutCommands = nodeVMs ?? throw new ArgumentNullException(nameof(nodeVMs));
        }

        /// <summary>
        /// Gets the connected pins.
        /// </summary>
        /// <value>The connected pins.</value>
        public ICollection<Tuple<PinVM, PinVM>> ConnectedPins
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="NodeVM"/> instances without commands.
        /// </summary>
        /// <value>The <see cref="NodeVM"/> instances without commands.</value>
        public ICollection<NodeVM> NodeVMsWithoutCommands
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="GlobalConfigSettings"/> of the class.
        /// </summary>
        /// <value>The settings of the class.</value>
        public GlobalConfigSettings Settings
        {
            get;
        }
    }
}
