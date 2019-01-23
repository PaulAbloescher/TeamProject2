using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YALS_WaspEdition.GlobalConfig
{
    public class GetColorWithGlobalConfigSettings : IGetColorForPin
    {
        private readonly GlobalConfigSettings settings;

        public GetColorWithGlobalConfigSettings(GlobalConfigSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Color GetColor(IPin item)
        {
            Color defaultColor = this.settings.DefaultColor;

            var pinType = item.GetType();
            var pinGenericTypes = pinType.GetGenericArguments();

            if (pinGenericTypes.Length == 0)
            {
                return defaultColor;
            }

            Type type = pinGenericTypes[0];

            if (item.Value == null)
            {
                return defaultColor;
            }

            if (type == typeof(string))
            {
                if (item.Value.Current == null)
                {
                    return defaultColor;
                }

                if (this.settings.StringValues.TryGetValue((string)item.Value.Current, out Color settingsColor))
                {
                    return settingsColor;
                }
            }

            if (type == typeof(int))
            {
                if (this.settings.IntValues.TryGetValue((int)item.Value.Current, out Color settingsColor))
                {
                    return settingsColor;
                }
            }

            if (type == typeof(bool))
            {
                if (this.settings.BoolValues.TryGetValue((bool)item.Value.Current, out Color settingsColor))
                {
                    return settingsColor;
                }
            }

            return defaultColor;
        }
    }
}
