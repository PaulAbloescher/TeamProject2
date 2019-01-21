using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YALS_WaspEdition.Model.Serialization
{
    public interface ICurrentStateSerializer
    {
        void Serialize(string outputPath, CurrentState state);

        CurrentState Deserialize(string path);
    }
}
