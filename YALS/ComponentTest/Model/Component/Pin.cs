using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using ComponentTest.Model.Value;

namespace ComponentTest.Model.Component
{
    public class Pin<T> : IPinGeneric<T>
    {
        public Pin(string label)
        {
            this.Label = label ?? throw new ArgumentNullException(nameof(label));
            this.Value = new ValueGeneric<T>();
        }

        public IValueGeneric<T> Value
        {
            get;

            set;
        }

        public string Label { get; set; }

        IValue IPin.Value
        {
            get
            {
                return this.Value;
            }

            set
            {
                this.Value = (IValueGeneric<T>)value;
            }
        }
    }
}
