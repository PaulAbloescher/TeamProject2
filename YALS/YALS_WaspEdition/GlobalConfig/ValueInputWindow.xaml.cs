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
    /// Interaction logic for ValueInputWindow.xaml
    /// </summary>
    public partial class ValueInputWindow : Window
    {
        public ValueInputWindow()
        {
            InitializeComponent();
        }
        public Color SelectedColor
        {
            get;
            private set;
        }

        private void AddValueClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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
