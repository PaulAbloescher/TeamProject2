// ----------------------------------------------------------------------- 
// <copyright file="DesignerComponentUC.xaml.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the DesignerComponentUC class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.Views.UserControls
{
    using System;
    using System.Windows.Controls;
    using YALS_WaspEdition.MyEventArgs;

    /// <summary>
    /// Interaction logic for DesignerComponentUC.xaml.
    /// </summary>
    public partial class DesignerComponentUC : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignerComponentUC"/> class.
        /// </summary>
        public DesignerComponentUC()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Occurs when a pin gets selected.
        /// </summary>
        public event EventHandler<PinSelectedEventArgs> PinSelected;

        /// <summary>
        /// Fires when a pin gets selected.
        /// </summary>
        /// <param name="args">The <see cref="PinSelectedEventArgs"/> instance containing the event data.</param>
        protected virtual void FirePinSelected(PinSelectedEventArgs args)
        {
            this.PinSelected?.Invoke(this, args);
        }
    }
}
