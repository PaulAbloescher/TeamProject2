
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
using YALS_WaspEdition.ViewModels;
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
            if (e.Data.GetDataPresent(typeof(NodeVM)))
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
            // TODO Fix components overlapping drag.
            var component = (NodeVM)e.Data.GetData(typeof(NodeVM));

            if (component != null)
            {
                Thumb thumb = new Thumb();
                thumb.DragDelta += Thumb_DragDelta;
                thumb.DataContext = component;
                var template = new ControlTemplate();
                var fec = new FrameworkElementFactory(typeof(ComponentUC));
                template.VisualTree = fec;
                thumb.Template = template;
                Canvas canvas = e.Source as Canvas;
                Point p = e.GetPosition(canvas);
                Canvas.SetLeft(thumb, p.X);
                Canvas.SetTop(thumb, p.Y);
                canvas.Children.Add(thumb);
            }
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            UIElement thumb = e.Source as UIElement;

            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
        }

        private void TreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ComponentTV.SelectedItem == null)
                return;

            var selectedItemType = ComponentTV.SelectedItem.GetType();

            if (selectedItemType != null && selectedItemType == typeof(NodeVM))
            {
                var nodeVM = ComponentTV.SelectedItem as NodeVM;
                var component = Activator.CreateInstance(nodeVM.Node.GetType()) as IDisplayableNode;
                var nodeVmNew = new NodeVM(component);
                DataObject data = new DataObject(typeof(NodeVM), nodeVmNew);
                DragDrop.DoDragDrop(ComponentTV, data, DragDropEffects.Copy);
            }
        }
    }
}
