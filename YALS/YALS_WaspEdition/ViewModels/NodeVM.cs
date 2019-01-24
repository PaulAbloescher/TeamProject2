// <copyright file="NodeVM.cs" company="KW Softworks">
//     Copyright (c) Paul-Noel Ablöscher. All rights reserved.
// </copyright>
// <summary>Represents the Node View Model.</summary>
// <author>Killerwasps</author>

namespace YALS_WaspEdition.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Windows.Input;

    using Shared;
    using YALS_WaspEdition.Commands;

    /// <summary>
    /// Represents the Node View Model.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    [Serializable]
    public class NodeVM : INotifyPropertyChanged, ISerializable
    {
        /// <summary>
        /// Represents the node.
        /// </summary>
        private readonly IDisplayableNode node;

        /// <summary>
        /// The input selected command.
        /// </summary>
        [NonSerialized]
        private ICommand inputSelectedCommand;

        /// <summary>
        /// The output selected command.
        /// </summary>
        [NonSerialized]
        private ICommand outputSelectedCommand;

        /// <summary>
        /// The remove command.
        /// </summary>
        [NonSerialized]
        private ICommand removeCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeVM"/> class.
        /// </summary>
        /// <param name="node">Represents the node.</param>
        /// <param name="outputSelectedCommand">The output selected command.</param>
        /// <param name="inputSelectedCommand">The input selected command.</param>
        public NodeVM(IDisplayableNode node, ICommand outputSelectedCommand, ICommand inputSelectedCommand)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
            this.node.PictureChanged += this.Node_PictureChanged;
            this.inputSelectedCommand = inputSelectedCommand ?? throw new ArgumentNullException(nameof(inputSelectedCommand));
            this.outputSelectedCommand = outputSelectedCommand ?? throw new ArgumentNullException(nameof(outputSelectedCommand)); 
            this.Setup();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeVM"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public NodeVM(SerializationInfo info, StreamingContext context)
        {
            this.node = (IDisplayableNode)info.GetValue("node", typeof(IDisplayableNode));
            this.Left = (double)info.GetValue("left", typeof(double));
            this.Top = (double)info.GetValue("top", typeof(double));
            this.Inputs = (ICollection<PinVM>)info.GetValue("inputs", typeof(ICollection<PinVM>));
            this.Outputs = (ICollection<PinVM>)info.GetValue("outputs", typeof(ICollection<PinVM>));
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <value>
        /// Represents the node.
        /// </value>
        public IDisplayableNode Node
        {
            get
            {
                return this.node;
            }
        }

        /// <summary>
        /// Gets the picture.
        /// </summary>
        /// <value>
        /// The picture.
        /// </value>
        public Bitmap Picture
        {
            get
            {
                return this.node.Picture;
            }
        }

        /// <summary>
        /// Gets the inputs.
        /// </summary>
        /// <value>
        /// The inputs.
        /// </value>
        public ICollection<PinVM> Inputs
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the outputs.
        /// </summary>
        /// <value>
        /// The outputs.
        /// </value>
        public ICollection<PinVM> Outputs
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>
        /// Represents the label.
        /// </value>
        public string Label
        {
            get
            {
                return this.node.Label;
            }
        }

        /// <summary>
        /// Gets or sets the left value.
        /// </summary>
        /// <value>
        /// Represents the left value.
        /// </value>
        public double Left
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets to top value.
        /// </summary>
        /// <value>
        /// Represents the top value.
        /// </value>
        public double Top
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the activate command.
        /// </summary>
        /// <value>
        /// The activate command.
        /// </value>
        public ICommand ActivateCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    this.Node.Activate();
                });
            }
        }

        /// <summary>
        /// Gets the input selected command.
        /// </summary>
        /// <value>
        /// The input selected command.
        /// </value>
        public ICommand InputSelectedCommand
        {
            get
            {
                return this.inputSelectedCommand;
            }

            private set
            {
                this.inputSelectedCommand = value ?? throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Gets the output selected command.
        /// </summary>
        /// <value>
        /// The output selected command.
        /// </value>
        public ICommand OutputSelectedCommand
        {
            get
            {
                return this.outputSelectedCommand;
            }

            private set
            {
                this.outputSelectedCommand = value ?? throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Gets or sets the remove command.
        /// </summary>
        /// <value>
        /// The remove command.
        /// </value>
        public ICommand RemoveCommand
        {
            get
            {
                return this.removeCommand;
            }

            set
            {
                this.removeCommand = value ?? throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                return this.Node.Description;
            }
        }

        /// <summary>
        /// Sets the selected command for pins.
        /// </summary>
        /// <param name="inputSelected">The input selected.</param>
        /// <param name="outputSelected">The output selected.</param>
        public void SetSelectedCommandForPins(ICommand inputSelected, ICommand outputSelected)
        {
            this.inputSelectedCommand = inputSelected ?? throw new ArgumentNullException(nameof(inputSelected));
            this.outputSelectedCommand = outputSelected ?? throw new ArgumentNullException(nameof(outputSelected));

            foreach (var pin in this.Inputs)
            {
                pin.SelectedCommand = inputSelected;
            }

            foreach (var pin in this.Outputs)
            {
                pin.SelectedCommand = outputSelected;
            }
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("node", this.Node);
            info.AddValue("left", this.Left);
            info.AddValue("top", this.Top);
            info.AddValue("inputs", this.Inputs);
            info.AddValue("outputs", this.Outputs);
        }

        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="name">Represents the name.</param>
        protected virtual void FirePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        private void Setup()
        {
            var inputPinVms = this.node.Inputs.Select(p => new PinVM(p, this.inputSelectedCommand)).ToList();
            var outputPinVms = this.node.Outputs.Select(p => new PinVM(p, this.outputSelectedCommand)).ToList();

            this.Inputs = inputPinVms;
            this.Outputs = outputPinVms;
        }

        /// <summary>
        /// Handles the PictureChanged event of the Node control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Node_PictureChanged(object sender, EventArgs e)
        {
            this.FirePropertyChanged(nameof(this.Picture));
        }
    }
}
