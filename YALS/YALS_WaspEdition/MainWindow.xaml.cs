// ---------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The logic directly working with the main window.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace YALS_WaspEdition
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Media;
    using Shared;
    using YALS_WaspEdition.Commands;
    using YALS_WaspEdition.Converters;
    using YALS_WaspEdition.GlobalConfig;
    using YALS_WaspEdition.MyEventArgs;
    using YALS_WaspEdition.ViewModels;
    using YALS_WaspEdition.Views.UserControls;

    /// <summary>
    /// The logic directly working with the main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.Setup();
        }

        /// <summary>
        /// Handles the DragOver event of the Canvas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.DragEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Drop event of the Canvas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.DragEventArgs"/> instance containing the event data.</param>
        private async void Canvas_Drop(object sender, System.Windows.DragEventArgs e)
        {
            // TODO Fix components overlapping drag.
            var component = (NodeVM)e.Data.GetData(typeof(NodeVM));
            MainVM mainVM = (MainVM)this.DataContext;

            if (component != null)
            {
                await mainVM.Manager.AddNodeAsync(component);
                Thumb thumb = new Thumb();
                thumb.DragDelta += this.Thumb_DragDelta;
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
                    Canvas.SetZIndex(thumb, -1);
                    component.Left = p.X;
                    component.Top = p.Y;
                    canvas.Children.Add(thumb);

                    // Learn a NodeVM how to remove itself.
                    component.RemoveCommand = new Command(async (obj) =>
                    {
                        await mainVM.Manager.Manager.StopAsync();
                        canvas.Children.Remove(thumb);
                        mainVM.Manager.Manager.Components.Remove(component.Node);
                        mainVM.Manager.RemoveNode(component);
                    });

                    thumb.Loaded += this.Thumb_Loaded;
                }
                catch (ArgumentNullException)
                {
                    System.Windows.MessageBox.Show("Do not drag components over other components!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        /// <summary>
        /// Handles the Loaded event of the Thumb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Thumb_Loaded(object sender, RoutedEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            this.SetInputOutputPinPositions(thumb);
        }

        /// <summary>
        /// Handles the DragDelta event of the Thumb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragDeltaEventArgs"/> instance containing the event data.</param>
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = e.Source as Thumb;
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

        /// <summary>
        /// Handles the MouseDown event of the TreeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void TreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = this.VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
            }

            MainVM mainVM = (MainVM)this.DataContext;

            if (ComponentTV.SelectedItem == null)
            {
                return;
            }

            var selectedItemType = ComponentTV.SelectedItem.GetType();

            if (selectedItemType != null && selectedItemType == typeof(NodeVM))
            {
                var nodeVM = ComponentTV.SelectedItem as NodeVM;
                var component = Activator.CreateInstance(nodeVM.Node.GetType()) as IDisplayableNode;
                var nodeVmNew = new NodeVM(
                    component,
                    new Command(obj =>
                {
                    PinVM pin = obj as PinVM;

                    if (pin == null)
                    {
                        return;
                    }

                    mainVM.CurrentSelectedOutput = pin;
                }),
                    new Command(obj =>
                {
                    PinVM pin = obj as PinVM;

                    if (pin == null)
                    {
                        return;
                    }

                    mainVM.CurrentSelectedInput = pin;
                }));

                System.Windows.DataObject data = new System.Windows.DataObject(typeof(NodeVM), nodeVmNew);
                DragDrop.DoDragDrop(this.ComponentTV, data, System.Windows.DragDropEffects.Copy);
            }
        }

        /// <summary>
        /// Searches the visual tree for the parent of the element.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The parent of the element.</returns>
        private TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
            {
                source = VisualTreeHelper.GetParent(source);
            }

            return source as TreeViewItem;
        }

        /// <summary>
        /// Sets the positions of the input and output pins.
        /// </summary>
        /// <param name="thumb">The thumb control of the element.</param>
        private void SetInputOutputPinPositions(Thumb thumb)
        {
            var test = VisualTreeHelper.GetChildrenCount(thumb);

            DesignerComponentUC compUc = VisualTreeHelper.GetChild(thumb, 0) as DesignerComponentUC;
            var icInputs = compUc.Inputs;
            var icOutputs = compUc.Outputs;
            var nodeVM = (NodeVM)thumb.DataContext;

            for (int i = 0; i < icInputs.Items.Count; i++)
            {
                UIElement uie = (UIElement)icInputs.ItemContainerGenerator.ContainerFromIndex(i);
                UIElement container = VisualTreeHelper.GetParent(uie) as UIElement;
                Point relativeLocation = uie.TranslatePoint(new Point(0, 0), container);
                
                var top = relativeLocation.Y + (uie.RenderSize.Height / 2);
                var left = relativeLocation.X + (uie.RenderSize.Width / 2);
                var currentPin = icInputs.Items[i] as PinVM;
                currentPin.Left = left + nodeVM.Left;
                currentPin.Top = top + nodeVM.Top;
            }

            for (int i = 0; i < icOutputs.Items.Count; i++)
            {
                UIElement uie = (UIElement)icOutputs.ItemContainerGenerator.ContainerFromIndex(i);
                UIElement container = VisualTreeHelper.GetParent(uie) as UIElement;
                Point relativeLocation = uie.TranslatePoint(new Point(0, 0), container);

                var top = relativeLocation.Y + (uie.RenderSize.Height / 2);
                var left = relativeLocation.X + (uie.RenderSize.Width / 2);
                var currentPin = icOutputs.Items[i] as PinVM;
                currentPin.Left = compUc.ActualWidth - left + nodeVM.Left;
                currentPin.Top = top + nodeVM.Top;
            }
        }

        /// <summary>
        /// Handles the Handler event of the OpenFile menu item.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OpenFile_Handler(object sender, RoutedEventArgs e)
        {
            this.OpenFile();
        }

        /// <summary>
        /// Handles the Handler event of the SaveFile menu item.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveFile_Handler(object sender, RoutedEventArgs e)
        {
            this.SaveFile();
        }

        /// <summary>
        /// Handles the Handler event of the SaveFileAs menu item.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveFileAs_Handler(object sender, RoutedEventArgs e)
        {
            this.SaveFileAs();
        }

        /// <summary>
        /// Opens a file.
        /// </summary>
        private async void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Wasp Files | *.wsp";

            try
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = dialog.FileName;
                    MainVM mainVM = (MainVM)this.DataContext;

                    await mainVM.LoadStateAsync(
                        fileName,
                        new Command(obj =>
                        {
                        PinVM pin = obj as PinVM;

                        if (pin == null)
                        {
                            return;
                        }

                        mainVM.CurrentSelectedInput = pin;
                    }),
                        new Command(obj =>
                    {
                        PinVM pin = obj as PinVM;

                        if (pin == null)
                        {
                            return;
                        }

                        mainVM.CurrentSelectedOutput = pin;
                    }));

                    foreach (NodeVM component in mainVM.Manager.NodeVMs)
                    {
                        Thumb thumb = new Thumb();
                        thumb.DragDelta += this.Thumb_DragDelta;
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
                            component.RemoveCommand = new Command(async (obj) =>
                            {
                                await mainVM.Manager.Manager.StopAsync();
                                canvas.Children.Remove(thumb);
                                mainVM.Manager.Manager.Components.Remove(component.Node);
                                mainVM.Manager.RemoveNode(component);
                            });
                            
                            thumb.Loaded += this.Thumb_Loaded;
                        }
                        catch (ArgumentNullException)
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

        /// <summary>
        /// Saves the current simulation.
        /// </summary>
        private void SaveFile()
        {
            var vm = (MainVM)this.DataContext;

            if (vm.LastSavedPath != null && File.Exists(vm.LastSavedPath))
            {
                vm.Save(vm.LastSavedPath);
            }
            else
            {
                this.SaveFileAs();
            }
        }

        /// <summary>
        /// Saves the current simulation under a given name.
        /// </summary>
        private void SaveFileAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.CheckFileExists = false;
            dialog.AddExtension = true;
            dialog.Filter = "Wasp Files | *.wsp";
            
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = dialog.FileName;
                MainVM mainVM = (MainVM)this.DataContext;
                mainVM.Save(fileName);
            }
        }

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        private void Setup()
        {
            var mainVm = (MainVM)this.DataContext;
            mainVm.NotificationRequested += this.MainVm_NotificationRequested;
            mainVm.OnNewRequested += this.MainVM_NewRequested;
            mainVm.OnSaveFileRequested += this.MainVM_SaveFileRequested;
            mainVm.OnOpenFileRequested += this.MainVM_OpenFileRequested;
        }

        /// <summary>
        /// Handles the NotificationRequested event of the MainVM control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotificationEventArgs"/> instance containing the event data.</param>
        private void MainVm_NotificationRequested(object sender, NotificationEventArgs e)
        {
            System.Windows.MessageBox.Show(e.Message, e.Caption, e.MessageBoxBtn, e.Icon);
        }

        /// <summary>
        /// Handles the NewRequested event of the MainVM control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainVM_NewRequested(object sender, EventArgs e)
        {
            var mainVm = (MainVM)this.DataContext;

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(this, "Are you sure you want to create a new file? Unsaved changes might be lost!", "Info", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                mainVm.Manager.Clear();
            }
        }

        /// <summary>
        /// Handles the SaveFileRequested event of the MainVM.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainVM_SaveFileRequested(object sender, EventArgs e)
        {
            this.SaveFile();
        }

        /// <summary>
        /// Handles the OpenFileRequested event of the MainVM.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainVM_OpenFileRequested(object sender, EventArgs e)
        {
            this.OpenFile();
        }

        /// <summary>
        /// Handles the MouseMove event of the MainCanvas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void MainCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MainVM mainVM = (MainVM)this.DataContext;

            if (mainVM != null && mainVM.FirstSelectedPin != null)
            {
                this.helperPath.Visibility = Visibility.Visible;
                var bezierConverter = this.FindResource("BezierConverter") as BezierConverter;
                var data = bezierConverter.Convert((int)mainVM.FirstSelectedPin.Left, (int)mainVM.FirstSelectedPin.Top, (int)e.GetPosition(mainCanvas).X - 1, (int)e.GetPosition(mainCanvas).Y - 1);

                this.helperPath.Data = data;
            }
            else
            {
                this.helperPath.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Called when the settings menu item is selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SettingsSelected(object sender, RoutedEventArgs e)
        {
            ColorConfigurationWindow configWindow = new ColorConfigurationWindow();
            configWindow.Owner = this;
            configWindow.Settings = ((MainVM)this.DataContext).Manager.Settings;
            configWindow.ShowDialog();
        }
    }
}
