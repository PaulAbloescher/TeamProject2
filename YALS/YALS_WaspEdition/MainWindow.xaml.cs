using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YALS_WaspEdition.Views.UserControls;

namespace YALS_WaspEdition
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Canvas_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void Canvas_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(IDisplayableNode)))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            var component = (IDisplayableNode)e.Data.GetData(typeof(IDisplayableNode));

            if (component != null)
            {
                Canvas canvas = e.Source as Canvas;
                Point p = e.GetPosition(canvas);            

                ContentControl c = new ContentControl();
                ComponentUC compUc = new ComponentUC();
                compUc.DataContext = component;
                c.Content = compUc;
                Canvas.SetLeft(c, p.X);
                Canvas.SetTop(c, p.Y);
                canvas.Children.Add(c);
            }
        }

        private void TmbDragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ComponentTV.SelectedItem != null)
            {
                var selectedItemType = ComponentTV.SelectedItem.GetType();

                if (selectedItemType != null && selectedItemType.GetInterfaces().Contains(typeof(IDisplayableNode)))
                {
                    var component = Activator.CreateInstance(selectedItemType) as IDisplayableNode;
                    DataObject data = new DataObject(typeof(IDisplayableNode), component);
                    DragDrop.DoDragDrop(ComponentTV, data, DragDropEffects.Copy);
                }
            }
        }
    }
}
