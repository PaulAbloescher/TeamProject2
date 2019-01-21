using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace YALS_WaspEdition.Model.Serialization
{
    public class BinaryCurrentStateSerializer : ICurrentStateSerializer
    {
        private readonly BinaryFormatter binaryFormatter;

        public BinaryCurrentStateSerializer(SerializationBinder binder)
        {
            this.binaryFormatter = new BinaryFormatter();
            binaryFormatter.Binder = binder ?? throw new ArgumentNullException(nameof(binder));
        }

        public CurrentState Deserialize(string path)
        {
            CurrentState state;

            using (Stream stream = new FileStream(path, FileMode.Open))
            {
                state = (CurrentState)this.binaryFormatter.Deserialize(stream);
            }

            return state;
        }

        public void Serialize(string outputPath, CurrentState state)
        {
            if (outputPath == null)
            {
                throw new ArgumentNullException(nameof(outputPath));
            }

            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            using (Stream stream = new FileStream(outputPath, FileMode.Create))
            {
                binaryFormatter.Serialize(stream, state);
            }
        }
    }
}
