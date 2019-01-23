using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YALS_WaspEdition.GlobalConfig
{
    [Serializable()]
    public class GlobalConfigSettings
    {
        // TODO make config generic
        // TODO maybe add settings for null strings
        public GlobalConfigSettings()
        {
            this.IntValues = new Dictionary<int, Color>();
            this.BoolValues = new Dictionary<bool, Color>();
            this.StringValues = new Dictionary<string, Color>();
        }

        public Color DefaultColor
        {
            get;
            set;
        }

        public Dictionary<int, Color> IntValues
        {
            get;
        }

        public Dictionary<bool, Color> BoolValues
        {
            get;
        }

        public Dictionary<string, Color> StringValues
        {
            get;
        }

        public static GlobalConfigSettings GetDefaultSettings()
        {
            GlobalConfigSettings settings = new GlobalConfigSettings();
            settings.BoolValues.Add(true, Color.FromRgb(0, 0, 255));
            settings.BoolValues.Add(false, Color.FromRgb(255, 0, 0));
            settings.DefaultColor = Color.FromRgb(0, 0, 0);
            return settings;
        }
    }
}
