namespace ThemeMixer.Themes.Weather
{
    public class WeatherValue : ThemePartBase
    {
        public ValueName Name;

        public WeatherValue() { }

        public WeatherValue(ValueName valueName) {
            Name = valueName;
        }

        public WeatherValue(string packageID, ValueName floatName) : base(packageID) {
            Name = floatName;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetThemeFromPackage(PackageID);
            if (metaData == null) return false;
            switch (Name) {
                case ValueName.MinTemperatureDay:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case ValueName.MaxTemperatureDay:
                    SetValue(metaData.maxTemperatureDay);
                    break;
                case ValueName.MinTemperatureNight:
                    SetValue(metaData.minTemperatureNight);
                    break;
                case ValueName.MaxTemperatureNight:
                    SetValue(metaData.maxTemperatureNight);
                    break;
                case ValueName.MinTemperatureRain:
                    SetValue(metaData.minTemperatureRain);
                    break;
                case ValueName.MaxTemperatureRain:
                    SetValue(metaData.maxTemperatureRain);
                    break;
                case ValueName.MinTemperatureFog:
                    SetValue(metaData.minTemperatureFog);
                    break;
                case ValueName.MaxTemperatureFog:
                    SetValue(metaData.maxTemperatureFog);
                    break;
                case ValueName.RainProbabilityDay:
                    SetValue(metaData.rainProbabilityDay);
                    break;
                case ValueName.RainProbabilityNight:
                    SetValue(metaData.rainProbabilityNight);
                    break;
                case ValueName.FogProbabilityDay:
                    SetValue(metaData.fogProbabilityDay);
                    break;
                case ValueName.FogProbabilityNight:
                    SetValue(metaData.fogProbabilityNight);
                    break;
                case ValueName.NorthernLightsProbability:
                    SetValue(metaData.northernLightsProbability);
                    break;
                default:
                    break;
            }
            return true;
        }

        protected override void LoadValue() {
            WeatherProperties properties = WeatherManager.instance.m_properties;
            switch (Name) {
                case ValueName.MinTemperatureDay:
                    properties.m_minTemperatureDay = (float)(CustomValue ?? Value);
                    break;
                case ValueName.MaxTemperatureDay:
                    properties.m_maxTemperatureDay = (float)(CustomValue ?? Value);
                    break;
                case ValueName.MinTemperatureNight:
                    properties.m_minTemperatureNight = (float)(CustomValue ?? Value);
                    break;
                case ValueName.MaxTemperatureNight:
                    properties.m_maxTemperatureNight = (float)(CustomValue ?? Value);
                    break;
                case ValueName.MinTemperatureRain:
                    properties.m_minTemperatureRain = (float)(CustomValue ?? Value);
                    break;
                case ValueName.MaxTemperatureRain:
                    properties.m_maxTemperatureRain = (float)(CustomValue ?? Value);
                    break;
                case ValueName.MinTemperatureFog:
                    properties.m_minTemperatureFog = (float)(CustomValue ?? Value);
                    break;
                case ValueName.MaxTemperatureFog:
                    properties.m_maxTemperatureFog = (float)(CustomValue ?? Value);
                    break;
                case ValueName.RainProbabilityDay:
                    properties.m_rainProbabilityDay = (int)(CustomValue ?? Value);
                    break;
                case ValueName.RainProbabilityNight:
                    properties.m_rainProbabilityNight = (int)(CustomValue ?? Value);
                    break;
                case ValueName.FogProbabilityDay:
                    properties.m_fogProbabilityDay = (int)(CustomValue ?? Value);
                    break;
                case ValueName.FogProbabilityNight:
                    properties.m_fogProbabilityNight = (int)(CustomValue ?? Value);
                    break;
                case ValueName.NorthernLightsProbability:
                    properties.m_northernLightsProbability = (int)(CustomValue ?? Value);
                    break;
                default:
                    break;
            }
        }

        public enum ValueName
        {
            MinTemperatureDay,
            MaxTemperatureDay,
            MinTemperatureNight,
            MaxTemperatureNight,
            MinTemperatureRain,
            MaxTemperatureRain,
            MinTemperatureFog,
            MaxTemperatureFog,

            RainProbabilityDay,
            RainProbabilityNight,
            FogProbabilityDay,
            FogProbabilityNight,
            NorthernLightsProbability,

            Count
        }
    }
}
