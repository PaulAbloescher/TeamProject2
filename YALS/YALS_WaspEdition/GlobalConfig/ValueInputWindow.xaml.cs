// ----------------------------------------------------------------------- 
// <copyright file="ValueInputWindow.xaml.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the ValueInputWindow class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.GlobalConfig
{
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for ValueInputWindow.xaml.
    /// </summary>
    public partial class ValueInputWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueInputWindow"/> class.
        /// </summary>
        public ValueInputWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the selected color.
        /// </summary>
        /// <value>
        /// The selected color.
        /// </value>
        public Color SelectedColor
        {
            get;
            private set;
        }

        /// <summary>
        /// Executes when add value is clicked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddValueClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Selects a new Color for the color text box with a <see cref="ColorDialog"/>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void ColorTextBoxMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ColorDialog dialog = new ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Color newColor = Color.FromRgb(dialog.Color.R, dialog.Color.G, dialog.Color.B);
                this.ColorTextBox.Background = new SolidColorBrush(newColor);
                this.SelectedColor = newColor;
                this.ColorTextBox.Text = string.Empty;
            }
        }
    }
}
