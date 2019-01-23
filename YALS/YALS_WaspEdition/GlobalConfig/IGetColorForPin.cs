using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using YALS_WaspEdition.ViewModels;

namespace YALS_WaspEdition.GlobalConfig
{
    public interface IGetColorForPin
    {
        Color GetColor(IPin item);
    }
}
