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
                try
                {
                    state = (CurrentState)this.binaryFormatter.Deserialize(stream);
                }
                catch(SerializationException e)
                {
                    throw new InvalidOperationException("The file does not have the right format or the components that are saved in the file are not loaded in the current application.", e);
                }
                catch(InvalidCastException e)
                {
                    throw new InvalidOperationException("The file does not have the right format.", e);
                }
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
