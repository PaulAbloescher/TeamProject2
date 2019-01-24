// <copyright file="PinVM.cs" company="KW Softworks">
//     Copyright (c) Paul-Noel Ablöscher. All rights reserved.
// </copyright>
// <summary>Represents the Pin View Model.</summary>
// <author>Killerwasps</author>

namespace YALS_WaspEdition.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Windows.Media;

    using Shared;
    using YALS_WaspEdition.GlobalConfig;

    /// <summary>
    /// Represents the Pin View Model.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    public class PinVM : INotifyPropertyChanged
    {
        /// <summary>
        /// The left value.
        /// </summary>
        private double left;

        /// <summary>
        /// The top value.
        /// </summary>
        private double top;

        /// <summary>
        /// The pin color.
        /// </summary>
        [NonSerialized]
        private Color pinColor;

        /// <summary>
        /// The selected command.
        /// </summary>
        [NonSerialized]
        private ICommand selectedCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="PinVM"/> class.
        /// </summary>
        /// <param name="pin">Represents the pin.</param>
        /// <param name="selectedCommand">The selected command.</param>
        public PinVM(IPin pin, ICommand selectedCommand)
        {
            this.Pin = pin ?? throw new ArgumentNullException(nameof(pin));
            this.selectedCommand = selectedCommand;
            this.PinColor = Color.FromRgb(0, 0, 0);
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the pin.
        /// </summary>
        /// <value>
        /// Represents the pin.
        /// </value>
        public IPin Pin
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the color of the pin.
        /// </summary>
        /// <value>
        /// The color of the pin.
        /// </value>
        public Color PinColor
        {
            get
            {
                return this.pinColor;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }   
                
                this.pinColor = value;
                this.FirePropertyChanged(nameof(this.PinColor));
            }
        }

        /// <summary>
        /// Gets or sets the selected command.
        /// </summary>
        /// <value>
        /// The selected command.
        /// </value>
        public ICommand SelectedCommand
        {
            get
            {
                return this.selectedCommand;
            }

            set
            {
                this.selectedCommand = value ?? throw new ArgumentNullException();
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
            get
            {
                return this.left;
            }

            set
            {
                this.left = value;
                this.FirePropertyChanged(nameof(this.Left));
            }
        }

        /// <summary>
        /// Gets or sets to top value.
        /// </summary>
        /// <value>
        /// The top value.
        /// </value>
        public double Top
        {
            get
            {
                return this.top;
            }

            set
            {
                this.top = value;
                this.FirePropertyChanged(nameof(this.Top));
            }
        }

        /// <summary>
        /// Applies the color rules.
        /// </summary>
        /// <param name="colorSetter">The color setter.</param>
        public void ApplyColorRules(IGetColorForPin colorSetter)
        {
            this.PinColor = colorSetter.GetColor(this.Pin);
        }

        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="name">Represents the name.</param>
        protected virtual void FirePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
