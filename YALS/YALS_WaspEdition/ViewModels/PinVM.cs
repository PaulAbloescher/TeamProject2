using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using YALS_WaspEdition.Commands;
using YALS_WaspEdition.GlobalConfig;

namespace YALS_WaspEdition.ViewModels
{
    [Serializable()]
    public class PinVM : INotifyPropertyChanged
    {
        private double left;
        private double top;

        [NonSerialized()]
        private ICommand selectedCommand;

        public PinVM(IPin pin, ICommand selectedCommand)
        {
            this.Pin = pin ?? throw new ArgumentNullException(nameof(pin));
            this.selectedCommand = selectedCommand;
            this.PinColor = Color.FromRgb(0, 0, 0);
        }

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public IPin Pin
        {
            get;
            private set;
        }

        public Color PinColor
        {
            get;
            private set;
        }

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

        public void ApplyColorRules(IGetColorForPin colorSetter)
        {
            this.PinColor = colorSetter.GetColor(this.Pin);
            this.FirePropertyChanged(nameof(this.PinColor));
        }

        protected virtual void FirePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
