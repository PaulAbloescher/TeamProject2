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
            this.IntValues = new Dictionary<int, SerializableColor>();
            this.BoolValues = new Dictionary<bool, SerializableColor>();
            this.StringValues = new Dictionary<string, SerializableColor>();
        }

        public SerializableColor DefaultColor
        {
            get;
            set;
        }

        public Dictionary<int, SerializableColor> IntValues
        {
            get;
        }

        public Dictionary<bool, SerializableColor> BoolValues
        {
            get;
        }

        public Dictionary<string, SerializableColor> StringValues
        {
            get;
        }

        public static GlobalConfigSettings GetDefaultSettings()
        {
            GlobalConfigSettings settings = new GlobalConfigSettings();
            settings.BoolValues.Add(true, new SerializableColor(0, 0, 255));
            settings.BoolValues.Add(false, new SerializableColor(255, 0, 0));
            settings.DefaultColor = new SerializableColor(0, 0, 0);
            return settings;
        }
    }
}
