
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YALS_WaspEdition.Commands;
using YALS_WaspEdition.Model.Serialization;
using YALS_WaspEdition.ViewModels;
using YALS_WaspEdition.Views.UserControls;
using YALS_WaspEdition.MyEventArgs;

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
            this.Setup();
        }

        private void Canvas_DragOver(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(NodeVM)))
            {
                e.Effects = System.Windows.DragDropEffects.Copy;
            }
            else
            {
                e.Effects = System.Windows.DragDropEffects.None;
            }

            e.Handled = true;
        }

        private async void Canvas_Drop(object sender, System.Windows.DragEventArgs e)
        {
            // TODO Fix components overlapping drag.
            var component = (NodeVM)e.Data.GetData(typeof(NodeVM));
            MainVM mainVM = (MainVM)this.DataContext;

            if (component != null)
            {
                mainVM.Manager.AddNode(component);
                Thumb thumb = new Thumb();
                thumb.DragDelta += Thumb_DragDelta;
                thumb.DataContext = component;
                var template = new ControlTemplate();
                var fec = new FrameworkElementFactory(typeof(DesignerComponentUC));
                template.VisualTree = fec;
                thumb.Template = template;

                try
                {
                    Canvas canvas = e.Source as Canvas;
                    Point p = e.GetPosition(canvas);
                    Canvas.SetLeft(thumb, p.X);
                    Canvas.SetTop(thumb, p.Y);
                    component.Left = p.X;
                    component.Top = p.Y;
                    canvas.Children.Add(thumb);

                    // Learn a NodeVM how to remove itself.
                    component.RemoveCommand = new Command(async (obj) => {
                        await mainVM.Manager.Manager.StopAsync();
                        canvas.Children.Remove(thumb);
                        mainVM.Manager.Manager.Components.Remove(component.Node);
                        mainVM.Manager.RemoveNode(component);
                    });

                    thumb.Loaded += Thumb_Loaded;
                }
                catch(ArgumentNullException)
                {
                    System.Windows.MessageBox.Show("Do not drag components over other components!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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
            // TODO remove
            MainVM mainVM = (MainVM)this.DataContext;
            var nodeVM = (NodeVM)thumb.DataContext;

            var nodeLeft = Canvas.GetLeft(thumb) + e.HorizontalChange;
            var nodeTop = Canvas.GetTop(thumb) + e.VerticalChange;

            if (nodeLeft < 0)
            {
                nodeLeft = 0;
            }
            else if (nodeLeft + thumb.ActualWidth > this.mainCanvas.ActualWidth) 
            {
                nodeLeft = this.mainCanvas.ActualWidth - thumb.ActualWidth;
            }

            if (nodeTop < 0)
            {
                nodeTop = 0;
            }
            else if (nodeTop + thumb.ActualHeight > this.mainCanvas.ActualHeight)
            {
                nodeTop = this.mainCanvas.ActualHeight - thumb.ActualHeight;
            }

            Canvas.SetLeft(thumb, nodeLeft);
            Canvas.SetTop(thumb, nodeTop);
            nodeVM.Left = Canvas.GetLeft(thumb);
            nodeVM.Top = Canvas.GetTop(thumb);
            this.SetInputOutputPinPositions(thumb);
        }

        private void TreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = this.VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
            }

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
                System.Windows.DataObject data = new System.Windows.DataObject(typeof(NodeVM), nodeVmNew);
                DragDrop.DoDragDrop(ComponentTV, data, System.Windows.DragDropEffects.Copy);
            }
        }

        private TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }

        private void SetInputOutputPinPositions(Thumb thumb)
        {
            var test = VisualTreeHelper.GetChildrenCount(thumb);

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

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Wasp Dateien | *.wsp";

            try
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = dialog.FileName;
                    MainVM mainVM = (MainVM)this.DataContext;

                    mainVM.LoadState(fileName, new Command(obj => {
                        PinVM pin = obj as PinVM;
                        if (pin == null)
                            return;
                        mainVM.CurrentSelectedInput = pin;
                    }), new Command(obj => {
                        PinVM pin = obj as PinVM;
                        if (pin == null)
                            return;
                        mainVM.CurrentSelectedOutput = pin;
                    }));

                    foreach (NodeVM component in mainVM.Manager.NodeVMs)
                    {
                        Thumb thumb = new Thumb();
                        thumb.DragDelta += Thumb_DragDelta;
                        thumb.DataContext = component;
                        var template = new ControlTemplate();
                        var fec = new FrameworkElementFactory(typeof(DesignerComponentUC));
                        template.VisualTree = fec;
                        thumb.Template = template;

                        try
                        {
                            Canvas canvas = this.mainCanvas;
                            Canvas.SetLeft(thumb, component.Left);
                            Canvas.SetTop(thumb, component.Top);
                            canvas.Children.Add(thumb);
                            component.RemoveCommand = new Command((obj) => {
                                canvas.Children.Remove(thumb);
                                mainVM.Manager.Manager.Components.Remove(component.Node);
                            });


                            thumb.Loaded += Thumb_Loaded;
                        }
                        catch (ArgumentNullException ex)
                        {
                            System.Windows.MessageBox.Show("Do not drag components over other components!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.CheckFileExists = false;
            dialog.AddExtension = true;
            dialog.Filter = "Wasp Dateien | *.wsp";


            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = dialog.FileName;
                MainVM mainVM = (MainVM)this.DataContext;
                mainVM.Save(fileName);
            }
        }

        private void Setup()
        {
            var mainVm = (MainVM)this.DataContext;
            mainVm.NotificationRequested += MainVm_NotificationRequested;
        }

        private void MainVm_NotificationRequested(object sender, NotificationEventArgs e)
        {
            System.Windows.MessageBox.Show(e.Message, e.Caption, e.MessageBoxBtn, e.Icon);
        }
    }
}
