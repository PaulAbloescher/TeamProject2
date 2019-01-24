// ----------------------------------------------------------------------- 
// <copyright file="PlayerUC.xaml.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the PlayerUC class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.Views.UserControls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for PlayerUC.xaml.
    /// </summary>
    public partial class PlayerUC : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUC"/> class.
        /// </summary>
        public PlayerUC()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.PlayBtn.IsChecked = false;
        }
    }
}
