
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
using YALS_WaspEdition.Commands;
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
            MainVM mainVM = (MainVM)this.DataContext;

            if (component != null)
            {
                mainVM.Manager.Manager.AddNode(component.Node);
                Thumb thumb = new Thumb();
                thumb.DragDelta += Thumb_DragDelta;
                thumb.DataContext = component;
                var template = new ControlTemplate();
                var fec = new FrameworkElementFactory(typeof(DesignerComponentUC));
                template.VisualTree = fec;
                thumb.Template = template;

                Canvas canvas = e.Source as Canvas;
                Point p = e.GetPosition(canvas);
                Canvas.SetLeft(thumb, p.X);
                Canvas.SetTop(thumb, p.Y);
                component.Left = p.X;
                component.Top = p.Y;
                canvas.Children.Add(thumb);

                // Learn a NodeVM how to remove itself.
                component.RemoveCommand = new Command((obj) => {
                    canvas.Children.Remove(thumb);
                    mainVM.Manager.Manager.Components.Remove(component.Node);
                });

                thumb.Loaded += Thumb_Loaded;
            }
        }

        private void Thumb_Loaded(object sender, RoutedEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            this.SetInputOutputPinPositions(thumb);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = e.Source as Thumb;
            var nodeVM = (NodeVM)thumb.DataContext;
            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
            nodeVM.Left = Canvas.GetLeft(thumb);
            nodeVM.Top = Canvas.GetTop(thumb);
            this.SetInputOutputPinPositions(thumb);
        }

        private void TreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainVM mainVM = (MainVM)this.DataContext;

            if (ComponentTV.SelectedItem == null)
                return;

            var selectedItemType = ComponentTV.SelectedItem.GetType();

            if (selectedItemType != null && selectedItemType == typeof(NodeVM))
            {
                var nodeVM = ComponentTV.SelectedItem as NodeVM;
                var component = Activator.CreateInstance(nodeVM.Node.GetType()) as IDisplayableNode;
                var nodeVmNew = new NodeVM(component, new Command(obj => {
                    PinVM pin = obj as PinVM;
                    if (pin == null)
                        return;
                    mainVM.CurrentSelectedOutput = pin;
                }), new Command(obj => {
                    PinVM pin = obj as PinVM;
                    if (pin == null)
                        return;
                    mainVM.CurrentSelectedInput = pin;
                }));
                DataObject data = new DataObject(typeof(NodeVM), nodeVmNew);
                DragDrop.DoDragDrop(ComponentTV, data, DragDropEffects.Copy);
            }
        }

        private void SetInputOutputPinPositions(Thumb thumb)
        {
            DesignerComponentUC compUc = VisualTreeHelper.GetChild(thumb, 0) as DesignerComponentUC;
            var icInputs = compUc.Inputs;
            var icOutputs = compUc.Outputs;
            var nodeVM = (NodeVM)thumb.DataContext;

            for (int i = 0; i < icInputs.Items.Count; i++)
            {
                UIElement uie = (UIElement) icInputs.ItemContainerGenerator.ContainerFromIndex(i);
                UIElement container = VisualTreeHelper.GetParent(uie) as UIElement;
                Point relativeLocation = uie.TranslatePoint(new Point(0, 0), container);
                
                var top = relativeLocation.Y + uie.RenderSize.Height / 2;
                var left = relativeLocation.X + uie.RenderSize.Width / 2;
                var currentPin = icInputs.Items[i] as PinVM;
                currentPin.Left = left + nodeVM.Left;
                currentPin.Top = top + nodeVM.Top;
            }

            for (int i = 0; i < icOutputs.Items.Count; i++)
            {
                UIElement uie = (UIElement)icOutputs.ItemContainerGenerator.ContainerFromIndex(i);
                UIElement container = VisualTreeHelper.GetParent(uie) as UIElement;
                Point relativeLocation = uie.TranslatePoint(new Point(0, 0), container);

                var top = relativeLocation.Y + uie.RenderSize.Height / 2;
                var left = relativeLocation.X + uie.RenderSize.Width / 2;
                var currentPin = icOutputs.Items[i] as PinVM;
                currentPin.Left = compUc.ActualWidth - left + nodeVM.Left;
                currentPin.Top = top + nodeVM.Top;
            }
        }
    }
}
