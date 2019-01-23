using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YALS_WaspEdition.GlobalConfig
{
    /// <summary>
    /// Interaction logic for ColorConfigurationWindow.xaml
    /// </summary>
    public partial class ColorConfigurationWindow : Window
    {
        private GlobalConfigSettings settings;

        public ColorConfigurationWindow()
        {
            InitializeComponent();
            this.IntSettingsView.SettingsListBox.SelectionChanged += IntSettingsSelected;
            this.StringSettingsView.SettingsListBox.SelectionChanged += StringSettingsSelected;
            this.BoolSettingsView.SettingsListBox.SelectionChanged += BoolSettingsSelected;
        }

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

        private void StringSettingsSelected(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = this.StringSettingsView.SettingsListBox.SelectedIndex;
            this.StringSettingsView.SettingsListBox.ItemsSource = this.UpdateColorOfValues<string>(this.settings.StringValues, selectedIndex);
            this.StringSettingsView.SettingsListBox.ItemsSource = this.settings.StringValues.OrderBy(item => item.Key);
            e.Handled = true;

        }

        private void IntSettingsSelected(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = this.IntSettingsView.SettingsListBox.SelectedIndex;
            this.IntSettingsView.SettingsListBox.ItemsSource = this.UpdateColorOfValues<int>(this.settings.IntValues, selectedIndex);
            this.IntSettingsView.SettingsListBox.ItemsSource = this.settings.IntValues.OrderBy(item => item.Key);
            e.Handled = true;

        }

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

        private void SetupValues()
        {
            this.BoolSettingsView.SettingsListBox.ItemsSource = this.Settings.BoolValues;
            this.IntSettingsView.SettingsListBox.ItemsSource = this.Settings.IntValues;
            this.StringSettingsView.SettingsListBox.ItemsSource = this.Settings.StringValues;
            this.DefaultColorBlock.Background = new SolidColorBrush(Color.FromRgb((byte)this.settings.DefaultColor.R, (byte)this.settings.DefaultColor.G, (byte)this.settings.DefaultColor.B));
        }

        private void AddIntValue(object sender, RoutedEventArgs e)
        {
            var valueInputWindow = new ValueInputWindow();

            if (valueInputWindow.ShowDialog() != null && valueInputWindow.SelectedColor != null)
            {
                if (int.TryParse(valueInputWindow.NewValueTextBox.Text, out int key))
                {
                    if (this.settings.IntValues.TryGetValue(key, out SerializableColor oldColor))
                    {
                        this.settings.IntValues.Remove(key);
                    }

                    this.settings.IntValues.Add(key, ConvertColorToSerializableColor(valueInputWindow.SelectedColor));
                    this.IntSettingsView.SettingsListBox.ItemsSource = this.settings.IntValues.OrderBy(value => value.Key);
                }
                else
                {
                    System.Windows.MessageBox.Show("The specified value is not an integer");
                }
            }

        }

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

                this.settings.StringValues.Add(key, ConvertColorToSerializableColor(valueInputWindow.SelectedColor));
                this.StringSettingsView.SettingsListBox.ItemsSource = this.settings.StringValues.OrderBy(value => value.Key);

            }

        }

        private SerializableColor ConvertColorToSerializableColor(Color color)
        {
            return new SerializableColor(color.R, color.G, color.B);
        }

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
