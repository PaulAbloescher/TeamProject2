using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YALS_WaspEdition.GlobalConfig
{
    [Serializable()]
    public class SerializableColor : ISerializable
    {

        public SerializableColor(int rValue, int gValue, int bValue)
        {
            if (rValue < 0 || rValue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(rValue));
            }
            if (gValue < 0 || gValue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(gValue));
            }
            if (bValue < 0 || bValue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(bValue));
            }

            this.R = rValue;
            this.G = gValue;
            this.B = bValue;
        }

        public SerializableColor(SerializationInfo info, StreamingContext context)
        {
            this.R = (int)info.GetValue("rValue", typeof(int));
            this.G = (int)info.GetValue("gValue", typeof(int));
            this.B = (int)info.GetValue("bValue", typeof(int));
        }

        public int R
        {
            get;
        }

        public int G
        {
            get;
        }

        public int B
        {
            get;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("rValue", this.R);
            info.AddValue("gValue", this.G);
            info.AddValue("bValue", this.B);
        }
    }
}
