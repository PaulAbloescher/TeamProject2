using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YALS_WaspEdition.GlobalConfig
{
    public class GetColorForTypes : IGetColorForItem
    {
        public GetColorForTypes(GlobalConfigSettings settings)
        {

        }

        public Color GetColor(object item)
        {
            throw new NotImplementedException();
        }
    }
}
