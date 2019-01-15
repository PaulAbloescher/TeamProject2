using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ComponentTest.Model.Value
{
    public class ValueGeneric<T> : IValueGeneric<T>
    {
        public T Value
        {
            get;
            set;
        }
        object IValue.Value
        {
            get
            {
                return this.Value;
            }

            set
            {
                this.Value = (T)value;
            }
        }
    }
}
