// ----------------------------------------------------------------------- 
// <copyright file="ColorConfigurationWindow.xaml.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the ColorConfigurationWindow class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.GlobalConfig
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for ColorConfigurationWindow.xaml.
    /// </summary>
    public partial class ColorConfigurationWindow : Window
    {
        /// <summary>
        /// The settings that get changed through the window.
        /// </summary>
        private GlobalConfigSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorConfigurationWindow"/> class.
        /// </summary>
        public ColorConfigurationWindow()
        {
            this.InitializeComponent();
            this.IntSettingsView.SettingsListBox.SelectionChanged += this.IntSettingsSelected;
            this.StringSettingsView.SettingsListBox.SelectionChanged += this.StringSettingsSelected;
            this.BoolSettingsView.SettingsListBox.SelectionChanged += this.BoolSettingsSelected;
        }

        /// <summary>
        /// Gets or sets the settings of the class.
        /// </summary>
        /// <value>
        /// The settings of the class.
        /// </value>
        public GlobalConfigSettings Settings
        {
            get
            {
                return this.settings;
            }

            set
            {
                this.settings = value ?? throw new ArgumentNullException(nameof(value));
                this.SetupValues();
            }
        }

        /// <summary>
        /// Sets the boolean settings.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void BoolSettingsSelected(object sender, SelectionChangedEventArgs e)
        {
            if (this.settings != null)
            {
                int selectedIndex = this.BoolSettingsView.SettingsListBox.SelectedIndex;
                this.BoolSettingsView.SettingsListBox.ItemsSource = this.UpdateColorOfValues<bool>(this.settings.BoolValues, selectedIndex);
                this.BoolSettingsView.SettingsListBox.ItemsSource = this.settings.BoolValues.Select(b => b);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Sets the strings settings.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void StringSettingsSelected(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = this.StringSettingsView.SettingsListBox.SelectedIndex;
            this.StringSettingsView.SettingsListBox.ItemsSource = this.UpdateColorOfValues<string>(this.settings.StringValues, selectedIndex);
            this.StringSettingsView.SettingsListBox.ItemsSource = this.settings.StringValues.OrderBy(item => item.Key);
            e.Handled = true;
        }

        /// <summary>
        /// Sets the integer settings.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void IntSettingsSelected(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = this.IntSettingsView.SettingsListBox.SelectedIndex;
            this.IntSettingsView.SettingsListBox.ItemsSource = this.UpdateColorOfValues<int>(this.settings.IntValues, selectedIndex);
            this.IntSettingsView.SettingsListBox.ItemsSource = this.settings.IntValues.OrderBy(item => item.Key);
            e.Handled = true;
        }

        /// <summary>
        /// Updates the color of values with a <see cref="ColorDialog"/> window.
        /// </summary>
        /// <typeparam name="T">The type for the keys of the dictionary.</typeparam>
        /// <param name="values">The values that get updated.</param>
        /// <param name="selectedIndex">Index of the selected value that gets updated.</param>
        /// <returns>The updated Dictionary.</returns>
        private Dictionary<T, SerializableColor> UpdateColorOfValues<T>(Dictionary<T, SerializableColor> values, int selectedIndex)
        {
            if (selectedIndex < values.Count() && selectedIndex >= 0)
            {
                ColorDialog dialog = new ColorDialog();

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.Drawing.Color selectedColor = dialog.Color;

                    var selectedItem = values.ElementAt(selectedIndex);
                    T key = selectedItem.Key;

                    values.Remove(key);
                    values.Add(key, new SerializableColor(selectedColor.R, selectedColor.G, selectedColor.B));
                }
            }

            return values;
        }

        /// <summary>
        /// Sets up the values.
        /// </summary>
        private void SetupValues()
        {
            this.BoolSettingsView.SettingsListBox.ItemsSource = this.Settings.BoolValues;
            this.IntSettingsView.SettingsListBox.ItemsSource = this.Settings.IntValues;
            this.StringSettingsView.SettingsListBox.ItemsSource = this.Settings.StringValues;
            this.DefaultColorBlock.Background = new SolidColorBrush(Color.FromRgb((byte)this.settings.DefaultColor.R, (byte)this.settings.DefaultColor.G, (byte)this.settings.DefaultColor.B));
        }

        /// <summary>
        /// Adds an integer value to the settings.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddIntValue(object sender, RoutedEventArgs e)
        {
            var valueInputWindow = new ValueInputWindow();

            if (valueInputWindow.ShowDialog() != null && valueInputWindow.SelectedColor != null)
            {
                int key = 0;

                if (int.TryParse(valueInputWindow.NewValueTextBox.Text, out key))
                {
                    if (this.settings.IntValues.TryGetValue(key, out SerializableColor oldColor))
                    {
                        this.settings.IntValues.Remove(key);
                    }

                    this.settings.IntValues.Add(key, this.ConvertColorToSerializableColor(valueInputWindow.SelectedColor));
                    this.IntSettingsView.SettingsListBox.ItemsSource = this.settings.IntValues.OrderBy(value => value.Key);
                }
                else
                {
                    System.Windows.MessageBox.Show("The specified value is not an integer");
                }
            }
        }

        /// <summary>
        /// Adds a string value to the settings.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddStringValue(object sender, RoutedEventArgs e)
        {
            var valueInputWindow = new ValueInputWindow();

            if (valueInputWindow.ShowDialog() != null && valueInputWindow.SelectedColor != null)
            {
                string key = valueInputWindow.NewValueTextBox.Text;

                if (this.settings.StringValues.TryGetValue(key, out SerializableColor oldColor))
                {
                    this.settings.StringValues.Remove(key);
                }

                this.settings.StringValues.Add(key, this.ConvertColorToSerializableColor(valueInputWindow.SelectedColor));
                this.StringSettingsView.SettingsListBox.ItemsSource = this.settings.StringValues.OrderBy(value => value.Key);
            }
        }

        /// <summary>
        /// Converts a <see cref="Color"/> a <see cref="SerializableColor"/>.
        /// </summary>
        /// <param name="color">The color that gets converted.</param>
        /// <returns>The converted color.</returns>
        private SerializableColor ConvertColorToSerializableColor(Color color)
        {
            return new SerializableColor(color.R, color.G, color.B);
        }

        /// <summary>
        /// Changes the settings for the default color with a <see cref="ColorDialog"/>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void DefaultColorBlockSelected(object sender, MouseButtonEventArgs e)
        {
            ColorDialog dialog = new ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color selectedColor = dialog.Color;
                this.DefaultColorBlock.Background = new SolidColorBrush(Color.FromRgb(selectedColor.R, selectedColor.G, selectedColor.B));
                this.Settings.DefaultColor = new SerializableColor(selectedColor.R, selectedColor.G, selectedColor.B);
            }
        }
    }
}
